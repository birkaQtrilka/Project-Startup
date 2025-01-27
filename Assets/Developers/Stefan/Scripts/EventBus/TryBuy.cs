using System;
/// <summary>
/// The publisher is creating this event, and the listener will Call OnAllow
/// </summary>
public readonly struct BookClicked : IEvent
{
    public BookData Book { get; }

    public BookClicked(BookData book)
    {
        Book = book;
    }
}
