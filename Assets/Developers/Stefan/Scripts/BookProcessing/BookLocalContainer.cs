using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(menuName = "BookContainer")]
public class BookLocalContainer : ScriptableObject
{
    [field: SerializeField] public AuthorData[] Authors { get; private set; }
    [field: SerializeField] public BookData[] Books { get; private set; }
    [field: SerializeField] public Dictionary<string, BookData> BooksDictionary { get; private set; }

    [SerializeField] int _booksToFetch = 2;
    [SerializeField] BookGetter _bookGetter;

    [SerializeField] bool _getBooks;
    [SerializeField] bool _removeAllBooks;
    [SerializeField] bool _updateDictionary;

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
        if (_updateDictionary)
        {
            _updateDictionary = false;
            UpdateDictionary();
        }
    }

    async Task GetBooks()
    {
        Books = await _bookGetter.FetchData(_booksToFetch);
        UpdateDictionary();
        AssetDatabase.Refresh();
    }

    public void UpdateDictionary()
    {
        BooksDictionary = Books.ToDictionary((b) => b.OLID);
    }
}
