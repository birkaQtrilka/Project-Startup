using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookGetter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _authors;
    [SerializeField] TextMeshProUGUI _publishDate;
    [SerializeField] TextMeshProUGUI _languages;
    [SerializeField] TextMeshProUGUI _publishers;
    [SerializeField] Image _bookImage;


    private void Start()
    {
        //_ = Test1();
        //_ = Test2();
    }

    public async Task<BookData> ParseToBook()
    {
        using HttpClient client = new();

        int i = 0;

        Debug.Log("Started");
        var endPoint = new Uri($"https://openlibrary.org/search.json?subject=english&sort=rating desc&limit=20");
        var result = await client.GetAsync(endPoint);
        Debug.Log("Got result");
        var json = await result.Content.ReadAsStringAsync();
        Debug.Log("parsed to json");

        var docs = JObject.Parse(json)["docs"];
        //getting only the first book
        var firstBookJSON = docs[i];

        string title = firstBookJSON["title"].ToString();

        string[] authors = firstBookJSON["author_name"].Select(a => a.ToString()).ToArray();

        DateTime publishDate = new(firstBookJSON["first_publish_year"].Value<int>(), 0, 0);
        float rating = firstBookJSON["ratings_average"].Value<float>();
        int ratingCount = firstBookJSON["ratings_count"].Value<int>();


        //putting the image
        var isbn = docs[i]["isbn"][0].ToString();
        var imageEndPoint = new Uri($"https://covers.openlibrary.org/b/isbn/" + isbn + ".jpg");
        Texture2D tex = new(2, 2);
        byte[] imageBytes = await client.GetByteArrayAsync(imageEndPoint);
        tex.LoadImage(imageBytes);
        Vector2 pivot = new(.5f, .5f);

        Sprite sprite = Sprite.Create(tex, new Rect(.0f, .0f, tex.width, tex.height), pivot, 100.0f);
        string genre = null;
        int numberOfPages = 0;
        int numberOfChapters = 0;
        string series = null;
        int editionNumber = 0;

        return new BookData(title, authors, publishDate, new Vector2(rating, 5), ratingCount, sprite,
             genre,numberOfPages,numberOfChapters,series,isbn,editionNumber
            );
    }

    //async Task Test2()
    //{
    //    //a way to mark a book as read
    //    //list of books that you have read
    //    //sort the books by rating or other filtering options
        
        
    //    using (HttpClient client = new())
    //    {
    //        Debug.Log("Started");
    //        var endPoint = new Uri($"https://openlibrary.org/search.json?subject=english&sort=rating desc&limit=20");
    //        var result = await client.GetAsync(endPoint);
    //        Debug.Log("Got result");
    //        var json = await result.Content.ReadAsStringAsync();
    //        Debug.Log("parsed to json");

    //        var docs = JObject.Parse(json)["docs"];
    //        //getting only the first book
    //        var firstBookJSON = docs[0];

    //        _title.text = firstBookJSON["title"].ToString();

    //        string authors = string.Join(", ", firstBookJSON["author_name"].Select(a => a.ToString()));
    //        _authors.text = authors;

    //        _publishDate.text = firstBookJSON["first_publish_year"].ToString();

    //        string publishers = string.Join(", ", firstBookJSON["publisher"].Select(a => a.ToString()).Take(3));
    //        _publishers.text = publishers.ToString();

    //        string languages = string.Join(", ", firstBookJSON["language"].Select(a => a.ToString()));
    //        _languages.text = languages.ToString();

    //        //putting the image
    //        var isbn = docs[0]["isbn"][0].ToString();
    //        var imageEndPoint = new Uri($"https://covers.openlibrary.org/b/isbn/"+ isbn + ".jpg");
    //        Texture2D tex = new Texture2D(2, 2);
    //        byte[] imageBytes = await client.GetByteArrayAsync(imageEndPoint);
    //        tex.LoadImage(imageBytes);

    //        Vector2 pivot = new Vector2(0.5f, 0.5f);
    //        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f);

    //        _bookImage.sprite = sprite;
    //    }
    //}
}
