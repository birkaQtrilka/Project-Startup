using System;
using System.Linq;
using System.Text;
using UnityEngine;

public class BookData 
{
    public string Title { get; private set; }
    public string[] Authors { get; private set; }
    public DateTime PublishDate { get; private set; }
    public Vector2 Rating { get; private set; }
    public int RatingCount { get; private set; }
    public Sprite Cover { get; private set; }
    public string Isbn { get; private set; }
    public int NumberOfPages { get; private set; }
    public int NumberOfChapters { get; private set; }
    public string Genre { get; private set; }
    public string[] Languages { get; private set; }
    public bool Read { get; set; }

    public BookData(string title, string[] authors, DateTime publishDate, 
        Vector2 rating,int ratingCount, Sprite cover,
        string genre, int numberOfPages, int numberOfChapters,
        string isbn, string[] languages)
    {
        Title = title;
        Authors = authors;
        PublishDate = publishDate;
        Rating = rating;
        Cover = cover;
        RatingCount = ratingCount;
        Isbn = isbn;
        Languages = languages;
        Genre = genre;
        NumberOfPages = numberOfPages;
        NumberOfChapters = numberOfChapters;
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
