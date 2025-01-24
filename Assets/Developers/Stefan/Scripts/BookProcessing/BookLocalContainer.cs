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

    [Header("For conveniency")]
    [SerializeField] UserData _user;
    [SerializeField] string _olidToOwn;
    [SerializeField] bool _ownBook;
    [SerializeField] bool _includeAllNotes;

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
        if(_ownBook)
        {
            _ownBook = false;
            _user.OwnABook(GetBookData(_olidToOwn));
        }

        if(_includeAllNotes)
        {
            _includeAllNotes = false;
            IncludeNotesInBooks();
        }
    }
    
    void IncludeNotesInBooks()
    {

        UserData[] users = Resources.FindObjectsOfTypeAll<UserData>();
        //I know this is really performance heavy but we won't have many objects so it should be fine for the prototype
        //in the future I would just use a database
        foreach (var user in users)
        {
            foreach (var post in user.Posts)
            {
                BookData book = GetBookData(post.OLID);
                if(!book.Notes.Any(n => n.PublishTime == post.PublishTime))
                    book.Notes.Add(post);
            }


        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public OwnedBook GetOwnedBook(UserData fromUser, string olid)
    {
        return fromUser.OwnedBooks.FirstOrDefault(b=>b.BookData.OLID == olid);
    }

    public BookData GetBookData(string olid)
    {
        return Books.FirstOrDefault(b => b.OLID == olid);
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
