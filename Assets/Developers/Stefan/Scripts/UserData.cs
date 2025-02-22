using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Users")]
public class UserData : ScriptableObject
{
    public UnityEvent<OwnedBook> OnBookOwn;

    public string NickName;
    public Sprite ProfilePicture;
    public string BirthDate;
    public string Title;
    public OwnedBook FavBook;
    [TextArea]
    public string Bio;

    public List<UserData> Friends;
    public List<OwnedBook> OwnedBooks;//library
    public List<BookDataSO> WishList;
    public List<PostData> Posts;


    //study how to make a prefference algorithm
    [SerializeField] int _searchVolume;
    int _nextSerchIndex;
    public List<string> SearchedStuff;
    public List<string> ClickedBookIds;

    [SerializeField]bool _ownBook;
    [SerializeField] BookDataSO _bookToOwn;
#if UNITY_EDITOR

    private void OnValidate()
    {
        if (_ownBook)
        {
            _ownBook = false;
            OwnABook(_bookToOwn);
        }
    }
#endif
    public void AddSeacherQuerry(string querry)
    {
        if (SearchedStuff.Contains(querry)) return;

        if(SearchedStuff.Count > _searchVolume)
        {
            if(_nextSerchIndex >= SearchedStuff.Count) _nextSerchIndex = 0;
            SearchedStuff[_nextSerchIndex] = querry;
            _nextSerchIndex++;
        }
        else
        {
            SearchedStuff.Add(querry);
            _nextSerchIndex = SearchedStuff.Count;
        }
    }

    public void OwnABook(BookDataSO bookData)
    {
        if (bookData == null)
        {
            Debug.LogError("BookData is null. Cannot own this book.");
            return;
        }

        for (int i = OwnedBooks.Count - 1; i >= 0; i--)
            if (OwnedBooks[i] == null) OwnedBooks.RemoveAt(i);

        if (OwnedBooks.Any(b => b.BookData.OLID == bookData.OLID)) return;

        Debug.Log("Owning book: " + bookData.Title);
        string path = "Assets/Developers/Stefan/ScriptableObjects/OwnedBooks/Resources";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Debug.Log("Directory created at: " + path);
        }
        string sanitizedTitle = string.Concat(bookData.Title.Split(Path.GetInvalidFileNameChars()));
#if UNITY_EDITOR
        string fullPath  = Path.Combine(path, sanitizedTitle + ".asset");
        OwnedBook ownedBook;

        if (!File.Exists(fullPath))
        {
            ownedBook = ScriptableObject.CreateInstance<OwnedBook>();
            AssetDatabase.CreateAsset(ownedBook, fullPath);
            ownedBook.Init(bookData);
            EditorUtility.SetDirty(ownedBook);
        }
        else
            ownedBook = Resources.Load<OwnedBook>(sanitizedTitle);
#endif
        OwnedBooks.Add(ownedBook);

        OnBookOwn?.Invoke(ownedBook);
    }

    public void DisownBook(BookDataSO book)
    {
        int index = -1;
        for (int i = 0; i < OwnedBooks.Count; i++)
            if (OwnedBooks[i].BookData == book)
            {
                index = i;
                break;
            }

        if (index == -1) return;
        var toDeleteBook = OwnedBooks[index];
        OwnedBooks.RemoveAt(index);

        ScriptableObject.DestroyImmediate(toDeleteBook,true);
    }

    public bool IsReadingBook(string olid)
    {
        return OwnedBooks.Any(b => b.BookData.OLID == olid);
    }
}
