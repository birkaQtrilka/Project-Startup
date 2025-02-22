using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class BookData 
{
    [field:SerializeField] public string Title { get; private set; }
    [field:SerializeField] public string[] Authors { get; private set; }
    [field:SerializeField] public string PublishDate { get; private set; }
    [field:SerializeField] public Vector2 Rating { get; private set; }
    [field:SerializeField] public int RatingCount { get; private set; }
    [field:SerializeField] public Sprite Cover { get; private set; }
    [field:SerializeField] public string Isbn { get; private set; }
    [field:SerializeField] public int NumberOfPages { get; private set; }
    [field:SerializeField] public int NumberOfChapters { get; private set; }
    [field:SerializeField] public string[] Genres { get; private set; }
    [field:SerializeField] public string OpenLibraryLink { get; private set; }
    [field:SerializeField] public string OLID { get; private set; }
    [field:SerializeField] public string[] Languages { get; private set; }
    [field:SerializeField, TextArea] public string Description { get; private set; }

    [field: Header("Made in the app")]
    [field: SerializeField] public List<ReviewData> LocalReviews { get; private set; }
    [field: SerializeField] public List<PostData> Notes { get; private set; }
    public BookData(string title, string[] authors, string publishDate, 
        Vector2 rating,int ratingCount, Sprite cover,
        string[] genre, int numberOfPages, int numberOfChapters,
        string isbn, string[] languages, string olid, string description)
    {
        OLID = olid;
        OpenLibraryLink = @"https://openlibrary.org/books/" + olid;
        Title = title;
        Authors = authors;
        PublishDate = publishDate;
        Rating = rating;
        Cover = cover;
        RatingCount = ratingCount;
        Isbn = isbn;
        Languages = languages;
        Genres = genre;
        NumberOfPages = numberOfPages == 0 ? 100 : numberOfPages;
        NumberOfChapters = numberOfChapters;
        Description = description;
    }

    public void SetSprite(Sprite sprite)
    {
        Cover = sprite;
    }

    public override string ToString()
    {
        var type = GetType();
        var fields = type.GetProperties();
        var sb = new StringBuilder();

        sb.AppendLine($"{type.Name} Details:");

        foreach (var field in fields)
        {
            var value = field.GetValue(this);
            string formattedValue;

            if (value is Array arrayValue)
            {
                formattedValue = string.Join(", ", arrayValue.Cast<object>());
            }
            else
            {
                formattedValue = value?.ToString() ?? "null";
            }

            sb.AppendLine($"{field.Name}: {formattedValue}\n");
        }

        return sb.ToString();
    }
}
