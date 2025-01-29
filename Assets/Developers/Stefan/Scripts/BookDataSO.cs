using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BookDataSO : ScriptableObject
{
    [field: SerializeField] public string Title { get; private set; }
    [field: SerializeField] public string[] Authors { get; private set; }
    [field: SerializeField] public string PublishDate { get; private set; }
    [field: SerializeField] public Vector2 Rating { get; private set; }
    [field: SerializeField] public int RatingCount { get; private set; }
    [field: SerializeField] public Sprite Cover { get; private set; }
    [field: SerializeField] public string Isbn { get; private set; }
    [field: SerializeField] public int NumberOfPages { get; private set; }
    [field: SerializeField] public int NumberOfChapters { get; private set; }
    [field: SerializeField] public string[] Genres { get; private set; }
    [field: SerializeField] public string OpenLibraryLink { get; private set; }
    [field: SerializeField] public string OLID { get; private set; }
    [field: SerializeField] public string[] Languages { get; private set; }
    [field: SerializeField, TextArea] public string Description { get; private set; }

    [field: Header("Made in the app")]
    [field: SerializeField] public List<ReviewData> LocalReviews { get; private set; }
    [field: SerializeField] public List<PostData> Notes { get; private set; }

    public void Init(BookData book)
    {
        OLID = book.OLID;
        OpenLibraryLink = book.OpenLibraryLink;
        Title = book.Title;
        Authors = book.Authors;
        PublishDate = book.PublishDate;
        Rating = book.Rating;
        Cover = book.Cover;
        RatingCount = book.RatingCount;
        Isbn = book.Isbn;
        Languages = book.Languages;
        Genres = book.Genres;
        NumberOfPages = book.NumberOfPages;
        NumberOfChapters = book.NumberOfChapters;
        Description = book.Description;
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
