using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _pageNum;
    [SerializeField] TextMeshProUGUI _author;
    [SerializeField] Image _pfp;
    [SerializeField] Image _bookCover;
    [SerializeField] Image _progressBarFill;

    public void Init(PostData post)
    {
        _text.text = post.Content;
        _author.text = post.UserData.NickName;
        _pageNum.text = "Page: " + post.Page.ToString();
        _pfp.sprite = post.UserData.ProfilePicture;

        OwnedBook ownedBook = post.UserData.OwnedBooks.FirstOrDefault(x => x.BookData.OLID == post.OLID);

        if (ownedBook == null) return;

        float fill = (float)ownedBook.CurrentPage / ownedBook.BookData.NumberOfPages;
        if(_progressBarFill != null)
            _progressBarFill.fillAmount = fill;

        if (_bookCover != null)
            _bookCover.sprite = ownedBook.BookData.Cover;
    }
}
