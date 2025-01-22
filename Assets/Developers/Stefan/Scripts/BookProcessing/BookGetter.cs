using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;
using System.Xml.Linq;
using UnityEngine.XR;
using System.IO;
using UnityEditor;
using System.Runtime.InteropServices;
[CreateAssetMenu(menuName ="BookGetter")]
public class BookGetter : ScriptableObject
{
    public string _imageLoadingPath = "Assets/Developers/Stefan/Resources/";

    CancellationTokenSource _cancellationTokenSource;
    CancellationToken _cancellationToken;

    [SerializeField] bool _newCancelationToken;

#if UNITY_EDITOR
    void OnValidate()
    {
        if(_newCancelationToken)
        {
            _newCancelationToken = false;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            Test();
        }
    }
#endif

    void OnEnable()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;
    }

    void OnDisable()
    {
        CancelGetting();
    }

    public void CancelGetting()
    {
        _cancellationTokenSource.Cancel();
    }

    public async Task<BookData[]> FetchData(int booksToFetch, string querry = "subject=english")
    {
        if(querry == null)
            querry = "subject=english";
        else
        {
            string formatedQuerry = string.Join('+', querry.TrimStart(' ').TrimEnd(' ').Split(' '));
            querry = "q=" + formatedQuerry;
        }


        using HttpClient client = new();
        var bookList = await GetBooksList(client, booksToFetch, querry);
        if (_cancellationToken.IsCancellationRequested)
        {
            Debug.Log("Canceled getting a book");
            return null;
        }

        int count = bookList.Count();
        List<Task<(BookData, byte[])>> bookTasks = new(count);

        for (int i = 0; i < count; i++)
        {
            int index = i; // Capture the index for the closure
            //unity textures need to be created in main thread
            bookTasks.Add(Task.Run(async () =>
            {
                (JToken edition, string key) = await GetEditionOfBook(bookList[index], client);
                return await ParseToBook(bookList[index], edition, key, client);
            }));
        }

        (BookData, byte[])[] books = await Task.WhenAll(bookTasks);
        if (_cancellationToken.IsCancellationRequested)
        {
            Debug.Log("Canceled getting a book");
            return null;
        }

        Debug.Log("finished getting all books");
        foreach ((BookData book, byte[] bytes) in books)
        {
            if (book == null) continue;

            //_ = LoadTexture(tex, bookAndByteArray.Item2);
            Sprite sprite = SaveTextureAsAsset(bytes, _imageLoadingPath, book.Title + ".jpg");
            book.SetSprite(sprite);
            Debug.Log(book);
        }
        
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;

        return books.Select(book => book.Item1).ToArray();
    }
    void Test()
    {

        string fileName = "Ἰλιάς";
        Debug.Log($"Test: {fileName}");
        var res = Resources.Load<Sprite>(fileName);
        Debug.Log("Test2: "+ res);
    }

    Sprite SaveTextureAsAsset(byte[] bytes, string path, string fileName)
    {
        path += fileName;
        try
        {
            Texture2D tex;
            if (!File.Exists(path))
            {
                tex = new(2, 2);
                tex.LoadImage(bytes);
#if UNITY_EDITOR
                System.IO.File.WriteAllBytes(path, bytes); // Refresh the AssetDatabase to show the new asset
                Debug.Log("loaded at path" + path);
                UnityEditor.AssetDatabase.Refresh();
                TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter; 
                if (textureImporter != null)
                { 
                    // Set the texture type to Sprite
                    textureImporter.textureType = TextureImporterType.Sprite; 
                    // Apply the changes
                    AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate); 
                    
                    Debug.Log("Texture type set to Sprite for: " + path); 
                } 
                else 
                { 
                    Debug.LogError("Failed to load TextureImporter for: " + path); 
                }
#endif

            }
            else
                Debug.Log("already exists at path" + path + "  with name: " + fileName);

            return Resources.Load<Sprite>(fileName.Split('.')[0]);
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
        }
        return null;
    }

    async Task LoadTexture(Texture2D tex, byte[] bytes)
    {
        await Task.Run(()=> tex.LoadImage(bytes), _cancellationToken);
    }

    public async Task<JToken> GetBooksList(HttpClient client, int limit, string querry)
    {
        try
        {
            Debug.Log("Started");
            var endPoint = new Uri($"https://openlibrary.org/search.json?" + querry + $"&sort=rating desc&limit={limit}" //);
                + "&fields=key,title,author_name,ratings_average,ratings_count,subject,language,isbn,description,editions");//difference between editions[] and edition_key?
            var result = await client.GetAsync(endPoint, _cancellationToken);
            if (_cancellationToken.IsCancellationRequested)
            {
                Debug.Log("Canceled getting a book");
                return null;
            }
            Debug.Log("Got result");
            var json = await result.Content.ReadAsStringAsync();
            return JObject.Parse(json)["docs"];

        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        return null;
    }

    public async Task<(JToken, string)> GetEditionOfBook(JToken firstBookJSON, HttpClient client)
    {
        try
        {
            var relevantEdition = firstBookJSON["editions"]["docs"][0];
            var key = relevantEdition["key"].ToString();
            var editionEndPoint = new Uri("https://openlibrary.org" + key + ".json");
            var response = await client.GetAsync(editionEndPoint, _cancellationToken);
            if (_cancellationToken.IsCancellationRequested)
            {
                Debug.Log("Canceled getting a book");
                return (null, null);
            }

            Debug.Log("Got editions");
            var editionString = await response.Content.ReadAsStringAsync();
            if (_cancellationToken.IsCancellationRequested)
            {
                Debug.Log("Canceled getting a book");
                return (null, null);
            }
            Debug.Log("Parsed editions");
            return (JObject.Parse(editionString), key);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        return (null, null);
    }

    public async Task<(BookData, byte[])> ParseToBook(JToken firstBookJSON, JToken editionJSON, string key, HttpClient client)
    {
        try
        {
            var olid = key.ToString().Split('/')[^1];

            string title = firstBookJSON["title"]?.ToString() ?? "no title";

            string[] authors = firstBookJSON["author_name"]?.Select(a => a.ToString()).ToArray() ?? null;
            string publishDate = editionJSON["publish_date"]?.ToString() ?? "";

            float rating = firstBookJSON["ratings_average"]?.Value<float>() ?? 0;
            int ratingCount = firstBookJSON["ratings_count"]?.Value<int>() ?? 0;

            //putting the image
            var isbn = firstBookJSON["isbn"]?[0].ToString() ?? "";//can't get isbn for specific book for now
            
            var imageEndPoint = new Uri($"https://covers.openlibrary.org/b/olid/" + olid + "-L.jpg");
            byte[] imageBytes = await client.GetByteArrayAsync(imageEndPoint);
            //44 is the response when the image is null
            //if(imageBytes.Length < 44) {
            //    imageEndPoint  = new Uri($"https://covers.openlibrary.org/b/olid/" + olid + "-M.jpg");
            //    imageBytes = await client.GetByteArrayAsync(imageEndPoint);
            //}
            string description = firstBookJSON["description"]?.ToString() ?? "no description";

            Debug.Log("byteArrayCount: " + imageBytes.Length);
            if (_cancellationToken.IsCancellationRequested)
            {
                Debug.Log("Canceled getting a book");
                return (null, null);
            }


            string[] genres = firstBookJSON["subject"]?.Select(a => a.ToString()).Take(5).ToArray() ?? null; ;
            int numberOfPages = editionJSON["number_of_pages"]?.Value<int>() ?? 0;
            int numberOfChapters = 0;

            //var test = editionJSON.ToString();
            numberOfChapters = editionJSON["table_of_contents"]?.Count() ?? 0;
            string[] languages = firstBookJSON["language"]?.Select(a => a.ToString()).ToArray() ?? null;

            return (new BookData(title, authors, publishDate, new Vector2(rating, 5), ratingCount, null,
                 genres, numberOfPages, numberOfChapters, isbn, languages, olid, description
            ), imageBytes);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        return (null, null);
    }
}
