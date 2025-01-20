using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;
using System.Xml.Linq;
[CreateAssetMenu(menuName ="BookGetter")]
public class BookGetter : ScriptableObject
{
    CancellationTokenSource _cancellationTokenSource;
    CancellationToken _cancellationToken;


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
        foreach ((BookData, byte[]) bookAndByteArray in books)
        {
            if (bookAndByteArray.Item1 == null) continue;
            Texture2D tex = new(2, 2);
            tex.LoadImage(bookAndByteArray.Item2);
            Vector2 pivot = new(.5f, .5f);
            Sprite sprite = Sprite.Create(tex, new Rect(.0f, .0f, tex.width, tex.height), pivot, 100.0f);
            bookAndByteArray.Item1.SetSprite(sprite);

            Debug.Log(bookAndByteArray.Item1);
        }

        return books.Select(book => book.Item1).ToArray();
    }

    public async Task<JToken> GetBooksList(HttpClient client, int limit, string querry)
    {
        Debug.Log("Started");
        var endPoint = new Uri($"https://openlibrary.org/search.json?"+ querry + $"&sort=rating desc&limit={limit}" //);
            + "&fields=key,title,author_name,ratings_average,ratings_count,subject,language,isbn,editions");//difference between editions[] and edition_key?
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

    public async Task<(JToken, string)> GetEditionOfBook(JToken firstBookJSON, HttpClient client)
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
            if (_cancellationToken.IsCancellationRequested)
            {
                Debug.Log("Canceled getting a book");
                return (null, null);
            }


            string genre = firstBookJSON["subject"]?[0].ToString() ?? "";
            int numberOfPages = editionJSON["number_of_pages"]?.Value<int>() ?? 0;
            int numberOfChapters = 0;

            //var test = editionJSON.ToString();
            numberOfChapters = editionJSON["table_of_contents"]?.Count() ?? 0;
            string[] languages = firstBookJSON["language"]?.Select(a => a.ToString()).ToArray() ?? null;

            return (new BookData(title, authors, publishDate, new Vector2(rating, 5), ratingCount, null,
                 genre, numberOfPages, numberOfChapters, isbn, languages, olid
            ), imageBytes);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        return (null, null);
    }
}
