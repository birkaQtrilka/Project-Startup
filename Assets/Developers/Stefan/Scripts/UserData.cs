using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Users")]
public class UserData : ScriptableObject
{
    public UnityEvent<OwnedBook> OnBookOwn;

    public string NickName;
    public Sprite ProfilePicture;

    public List<UserData> Friends;
    public List<OwnedBook> OwnedBooks;//library
    public List<BookData> WishList;
    public List<PostData> Posts;


    //study how to make a prefference algorithm
    [SerializeField] int _searchVolume;
    int _nextSerchIndex;
    public List<string> SearchedStuff;
    public List<string> ClickedBookIds;

    //[SerializeField]bool add;
    //int test;

    //private void OnValidate()
    //{
    //    if(add)
    //    {
    //        add = false;
    //        AddSeacherQuerry(test.ToString());
    //        test++;
    //    }
    //}

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

    public void OwnABook(BookData bookData)
    {
        for (int i = 0; i < OwnedBooks.Count; i++)
            if (OwnedBooks[i] == null) OwnedBooks.RemoveAt(i--);

        if (OwnedBooks.Any(b => b.BookData.OLID == bookData.OLID)) return;
        Debug.Log("Owning book: " + bookData.Title);
        OwnedBook ownedBook = ScriptableObject.CreateInstance<OwnedBook>();
        string path = "Assets/Developers/Stefan/ScriptableObjects/OwnedBooks";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Debug.Log("Directory created at: " + path);
        }
        AssetDatabase.CreateAsset(ownedBook, path + "/" + bookData.Title +".asset");
        AssetDatabase.SaveAssets();
        OwnedBooks.Add(ownedBook);

        ownedBook.Init(bookData);

        OnBookOwn?.Invoke(ownedBook);
    }

    public void DisownBook(BookData book)
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
