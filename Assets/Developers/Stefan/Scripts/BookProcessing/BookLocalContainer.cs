using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "BookContainer")]
public class BookLocalContainer : ScriptableObject
{
    [field: SerializeField] public BookData[] Books { get; private set; }

    [SerializeField] int _booksToFetch = 2;
    [SerializeField] BookGetter _bookGetter;

    [SerializeField] bool _getBooks;
    [SerializeField] bool _removeAllBooks;

    void OnValidate()
    {
        if (_removeAllBooks)
        {
            _removeAllBooks = false;

            Books = null;
        }
        if (_getBooks)
        {
            _getBooks = false;

            _ = GetBooks();
        }
    }

    async Task GetBooks()
    {
        Books = await _bookGetter.FetchData(_booksToFetch);
    }

    
}
