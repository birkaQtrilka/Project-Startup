using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class LibraryList
{
    public string Name;
    public List<OwnedBook> OwnedBooks;
}

public class LibraryListProvider : MonoBehaviour
{
    [field: SerializeField] public UnityEvent<LibraryList> OnListAdded { get; private set; }

    [SerializeField] List<LibraryList> _lists;

    [SerializeField] UserData _currentUser;
    [SerializeField] bool _showBooksOfCurrentUser;
    [SerializeField] List<OwnedBook> _ownedBooksOfUser;

    void OnValidate()
    {
        if(_showBooksOfCurrentUser)
        {
            _showBooksOfCurrentUser = false;
            _ownedBooksOfUser = new(_currentUser.OwnedBooks);
        }
    }

    public List<LibraryList> GetList()
    {
        return _lists;
    }

    public void AddList(string name)
    {
        var newList = new LibraryList() { Name = name, OwnedBooks = new() };
        _lists.Add(newList);
        OnListAdded?.Invoke(newList);
    }

    public void AddToList(string name, OwnedBook book)
    {
        var list = GetList(name);
        list.OwnedBooks.Add(book);
    }

    public void RemoveFromList(string name, OwnedBook book)
    {
        var list = GetList(name);
        list.OwnedBooks.Remove(book);
    }

    public bool IsInList(string name, OwnedBook book)
    { 
        return GetList(name).OwnedBooks.Contains(book);
    }

    public LibraryList GetList(string list)
    {
        return _lists.FirstOrDefault(l => l.Name == list);
    }

    public void AddToLastList(OwnedBook book)
    {
        _lists[^1].OwnedBooks.Add(book);
    }

    public void RemoveFromLastList(OwnedBook book)
    {
        _lists[^1].OwnedBooks.Remove(book);

    }

    public bool IsInLastList(OwnedBook book)
    {
        return _lists[^1].OwnedBooks.Contains(book);
    }
}
