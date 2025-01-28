using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;

[CreateAssetMenu(menuName = "BookContainer")]
public class BookLocalContainer : ScriptableObject
{
    [field: SerializeField] public AuthorData[] Authors { get; private set; }
    [field: SerializeField] public BookData[] Books { get; private set; }

    [SerializeField] int _booksToFetch = 2;
    [SerializeField] string _searchQuerry;
    [SerializeField] BookGetter _bookGetter;
    [SerializeField] BookLocalContainer _addContentsTo;

    [SerializeField] bool _getBooks;
    [SerializeField] bool _removeAllBooks;
    [SerializeField] bool _concatAfterGettingBooks;

    [Header("For conveniency")]
    [SerializeField] UserData _user;
    [SerializeField] BookDataSO _bookToOwn;
    [SerializeField] bool _ownBook;
    [SerializeField] bool _includeAllNotes;
    [SerializeField] string _path;
    [SerializeField] bool _generateIntoScriptableObjects;

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

            _ = GetBooks(_searchQuerry);
        }
        if (_addContentsTo != null)
        {
            var copy = _addContentsTo;
            _addContentsTo = null;
            if (copy == this) return;

            copy.Books = copy.Books.Concat(Books).ToArray();
        }
        if(_ownBook)
        {
            _ownBook = false;
            _user.OwnABook(_bookToOwn);
        }

        if(_includeAllNotes)
        {
            _includeAllNotes = false;
            IncludeNotesInBooks();
        }

        if(_generateIntoScriptableObjects)
        {
            _generateIntoScriptableObjects = false;
            GenerateScriptableObjectsFromBooks();
        }
    }
    
    public void IncludeNotesInBooks()
    {
        UserData[] users = Resources.FindObjectsOfTypeAll<UserData>();
        //I know this is really performance heavy but we won't have many objects so it should be fine for the prototype
        //in the future I would just use a database
        foreach (var user in users)
        {
            foreach (var post in user.Posts)
            {
                BookData book = GetBookData(post.OLID);
                if(!book.Notes.Any(n => n.ID == post.ID))
                    book.Notes.Add(post);
            }


        }
#if UNITY_EDITOR
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
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
        if(_concatAfterGettingBooks)
        Books.Concat(await _bookGetter.FetchData(_booksToFetch));
            else
        Books = await _bookGetter.FetchData(_booksToFetch);
    }

    async Task GetBooks(string querry)
    {
        if (_concatAfterGettingBooks)
            Books = Books.Concat(await _bookGetter.FetchData(_booksToFetch, querry)).ToArray();
        else
            Books = await _bookGetter.FetchData(_booksToFetch, querry);

    }

    void GenerateScriptableObjectsFromBooks()
    {
        foreach (var book in Books)
        {
            var so = ScriptableObject.CreateInstance<BookDataSO>();
            so.Init(book);

            string sanitizedTitle = string.Concat(book.Title.Split(Path.GetInvalidFileNameChars()));
            AssetDatabase.CreateAsset(so, _path + sanitizedTitle + ".asset");
        }
        AssetDatabase.SaveAssets();
    }
}
