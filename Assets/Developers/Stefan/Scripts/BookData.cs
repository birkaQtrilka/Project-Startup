using System;
using UnityEngine;

public class BookData 
{
    public string Title { get; private set; }
    public string[] Authors { get; private set; }
    public DateTime PublishDate { get; private set; }
    public Vector2 Rating { get; private set; }
    public int RatingCount { get; private set; }
    public Sprite Cover { get; private set; }

    public BookData(string title, string[] authors, DateTime publishDate, Vector2 rating,int ratingCount, Sprite cover )
    {
        Title = title;
        Authors = authors;
        PublishDate = publishDate;
        Rating = rating;
        Cover = cover;
        RatingCount = ratingCount;
    }
}
