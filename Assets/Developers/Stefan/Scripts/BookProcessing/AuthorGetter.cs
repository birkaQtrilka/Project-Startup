using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using UnityEditor;
using UnityEngine;
using System.IO;

public class AuthorGetter : MonoBehaviour
{
    //    public string _imageLoadingPath = "Assets/Developers/Stefan/Resources/Authors/";

    //    CancellationTokenSource _cancellationTokenSource;
    //    CancellationToken _cancellationToken;

    //    [SerializeField] bool _newCancelationToken;

    //#if UNITY_EDITOR
    //    void OnValidate()
    //    {
    //        if (_newCancelationToken)
    //        {
    //            _newCancelationToken = false;
    //            _cancellationTokenSource = new CancellationTokenSource();
    //            _cancellationToken = _cancellationTokenSource.Token;

    //        }
    //    }
    //#endif

    //    void OnEnable()
    //    {
    //        _cancellationTokenSource = new CancellationTokenSource();
    //        _cancellationToken = _cancellationTokenSource.Token;
    //    }

    //    void OnDisable()
    //    {
    //        CancelGetting();
    //    }

    //    public void CancelGetting()
    //    {
    //        _cancellationTokenSource.Cancel();
    //    }

    //    public async Task<AuthorData[]> FetchData(int booksToFetch, string querry = "j+k+rowling")
    //    {
    //        using HttpClient client = new();
    //        var bookList = await GetBooksList(client, booksToFetch, querry);
    //        if (_cancellationToken.IsCancellationRequested)
    //        {
    //            Debug.Log("Canceled getting a book");
    //            return null;
    //        }

    //        int count = bookList.Count();
    //        List<Task<(AuthorData, byte[])>> bookTasks = new(count);

    //        for (int i = 0; i < count; i++)
    //        {
    //            int index = i; // Capture the index for the closure
    //            //unity textures need to be created in main thread
    //            bookTasks.Add(Task.Run(async () =>
    //            {
    //                return await ParseToAuthor(bookList[index], client);
    //            }));
    //        }

    //        (AuthorData, byte[])[] books = await Task.WhenAll(bookTasks);
    //        if (_cancellationToken.IsCancellationRequested)
    //        {
    //            Debug.Log("Canceled getting a book");
    //            return null;
    //        }

    //        Debug.Log("finished getting all books");
    //        foreach ((AuthorData author, byte[] bytes) in books)
    //        {
    //            if (author == null) continue;

    //            //_ = LoadTexture(tex, bookAndByteArray.Item2);
    //            Sprite sprite = SaveTextureAsAsset(bytes, _imageLoadingPath, author.Name + ".jpg");
    //            author.Image = sprite;
    //            Debug.Log(author);
    //        }

    //        _cancellationTokenSource = new CancellationTokenSource();
    //        _cancellationToken = _cancellationTokenSource.Token;

    //        return books.Select(book => book.Item1).ToArray();
    //    }


    //    Sprite SaveTextureAsAsset(byte[] bytes, string path, string fileName)
    //    {
    //        path += fileName;
    //        try
    //        {
    //            Texture2D tex;
    //            if (!File.Exists(path))
    //            {
    //                tex = new(2, 2);
    //                tex.LoadImage(bytes);
    //#if UNITY_EDITOR
    //                System.IO.File.WriteAllBytes(path, bytes); // Refresh the AssetDatabase to show the new asset
    //                Debug.Log("loaded at path" + path);
    //                UnityEditor.AssetDatabase.Refresh();
    //                TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
    //                if (textureImporter != null)
    //                {
    //                    // Set the texture type to Sprite
    //                    textureImporter.textureType = TextureImporterType.Sprite;
    //                    // Apply the changes
    //                    AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

    //                    Debug.Log("Texture type set to Sprite for: " + path);
    //                }
    //                else
    //                {
    //                    Debug.LogError("Failed to load TextureImporter for: " + path);
    //                }
    //#endif

    //            }
    //            else
    //                Debug.Log("already exists at path" + path + "  with name: " + fileName);
    //            //might be a problem. Probably in runtime mode resorces folder can't be modified
    //            return Resources.Load<Sprite>(fileName.Split('.')[0]);
    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.LogError(ex);
    //        }
    //        return null;
    //    }


    //    public async Task<JToken> GetBooksList(HttpClient client, int limit, string querry)
    //    {
    //        try
    //        {
    //            Debug.Log("Started");
    //            var endPoint = new Uri($"https://openlibrary.org/search/authors.json?q={querry}" + $"&limit={limit}" );
    //            var result = await client.GetAsync(endPoint, _cancellationToken);
    //            if (_cancellationToken.IsCancellationRequested)
    //            {
    //                Debug.Log("Canceled getting a book");
    //                return null;
    //            }
    //            Debug.Log("Got result");
    //            var json = await result.Content.ReadAsStringAsync();
    //            return JObject.Parse(json)["docs"];

    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.Log(ex.Message);
    //        }
    //        return null;
    //    }

    //    public async Task<(AuthorData, byte[])> ParseToAuthor(JToken firstBookJSON, HttpClient client)
    //    {
    //        try
    //        {
    //            var olid = key.ToString().Split('/')[^1];

    //            string title = firstBookJSON["title"]?.ToString() ?? "no title";

    //            string[] authors = firstBookJSON["author_name"]?.Select(a => a.ToString()).ToArray() ?? null;

    //            float rating = firstBookJSON["ratings_average"]?.Value<float>() ?? 0;
    //            int ratingCount = firstBookJSON["ratings_count"]?.Value<int>() ?? 0;

    //            var imageEndPoint = new Uri($"https://covers.openlibrary.org/b/olid/" + olid + "-L.jpg");
    //            byte[] imageBytes = await client.GetByteArrayAsync(imageEndPoint);
    //            //44 is the response when the image is null
    //            //if(imageBytes.Length < 44) {
    //            //    imageEndPoint  = new Uri($"https://covers.openlibrary.org/b/olid/" + olid + "-M.jpg");
    //            //    imageBytes = await client.GetByteArrayAsync(imageEndPoint);
    //            //}
    //            string description = firstBookJSON["description"]?.ToString() ?? "no description";

    //            Debug.Log("byteArrayCount: " + imageBytes.Length);
    //            if (_cancellationToken.IsCancellationRequested)
    //            {
    //                Debug.Log("Canceled getting a book");
    //                return (null, null);
    //            }



    //            //var test = editionJSON.ToString();
    //            string[] languages = firstBookJSON["language"]?.Select(a => a.ToString()).ToArray() ?? null;

    //            return (new AuthorData(), imageBytes);
    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.LogError(ex.Message);
    //        }
    //        return (null, null);
    //    }
}
