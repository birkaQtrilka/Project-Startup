using System;
//using System.Drawing.Common;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
//using static System.Net.Mime.MediaTypeNames;

public class BookGetter : MonoBehaviour
{
    private void Start()
    {
        _ = Test();
    }

    async Task Test()
    {
        using (HttpClient client = new())
        {
            Debug.Log("Started");
            var endPoint = new Uri("https://openlibrary.org/api/books?bibkeys=ISBN:9780980200447&jscmd=data&format=json");
            var result = await client.GetAsync(endPoint);
            Debug.Log("Got result");
            var json = await result.Content.ReadAsStringAsync();
            Debug.Log("parsed to json");


            var imageEP = new Uri("https://covers.openlibrary.org/b/id/240726-S.jpg");
            byte[] imageBytes = await client.GetByteArrayAsync(imageEP);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                //Image image = System.Drawing.Image.FromStream(imageBytes);
            }
            Debug.Log(json);
        }
    }
}
