using System;
/// <summary>
/// The publisher is creating this event, and the listener will Call OnAllow
/// </summary>
public readonly struct BookClicked : IEvent
{
    public BookDataSO Book { get; }

    public BookClicked(BookDataSO book)
    {
        Book = book;
    }
}
