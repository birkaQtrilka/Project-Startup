using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.PackageManager;
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
        _ = Test2();
    }

    async Task Test1()
    {
        using (HttpClient client = new())
        {
            Debug.Log("Started");
            var endPoint = new Uri("https://openlibrary.org/api/books?bibkeys=ISBN:9780980200447&jscmd=data&format=json");
            var result = await client.GetAsync(endPoint);
            Debug.Log("Got result");
            var json = await result.Content.ReadAsStringAsync();
            Debug.Log("parsed to json");
            
            Debug.Log(json);
            var serializedJSON = JObject.Parse(json);
            var firstBookJSON = serializedJSON["ISBN:9780980200447"];

            _title.text = firstBookJSON["title"].ToString();

            string authors = string.Join(", ", firstBookJSON["authors"].Select(a => a["name"].ToString()));
            _authors.text = authors;
            
            _publishDate.text = firstBookJSON["publish_date"].ToString();
            
            string publishers = string.Join(", ", firstBookJSON["publishers"].Select(a => a["name"].ToString()));
            _publishers.text = publishers.ToString();

            Texture2D tex = new Texture2D(2, 2);

            var imageEP = new Uri(firstBookJSON["cover"]["large"].ToString());
            byte[] imageBytes = await client.GetByteArrayAsync(imageEP);

            tex.LoadImage(imageBytes);

            Vector2 pivot = new Vector2(0.5f, 0.5f);
            Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f);

            _bookImage.sprite = sprite;
        }
    }

    async Task Test2()
    {
        //a way to mark a book as read
        //list of books that you have read
        //sort the books by rating or other filtering options
        
        
        using (HttpClient client = new())
        {
            Debug.Log("Started");
            var endPoint = new Uri($"https://openlibrary.org/search.json?subject=english&sort=rating desc&limit=20");
            var result = await client.GetAsync(endPoint);
            Debug.Log("Got result");
            var json = await result.Content.ReadAsStringAsync();
            Debug.Log("parsed to json");

            var docs = JObject.Parse(json)["docs"];
            //getting only the first book
            var firstBookJSON = docs[0];

            _title.text = firstBookJSON["title"].ToString();

            string authors = string.Join(", ", firstBookJSON["author_name"].Select(a => a.ToString()));
            _authors.text = authors;

            _publishDate.text = firstBookJSON["first_publish_year"].ToString();

            string publishers = string.Join(", ", firstBookJSON["publisher"].Select(a => a.ToString()).Take(3));
            _publishers.text = publishers.ToString();

            string languages = string.Join(", ", firstBookJSON["language"].Select(a => a.ToString()));
            _languages.text = languages.ToString();

            //putting the image
            var isbn = docs[0]["isbn"][0].ToString();
            var imageEndPoint = new Uri($"https://covers.openlibrary.org/b/isbn/"+ isbn + ".jpg");
            Texture2D tex = new Texture2D(2, 2);
            byte[] imageBytes = await client.GetByteArrayAsync(imageEndPoint);
            tex.LoadImage(imageBytes);

            Vector2 pivot = new Vector2(0.5f, 0.5f);
            Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f);

            _bookImage.sprite = sprite;
        }
    }
}
