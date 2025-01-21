using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailedBookUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _description;
    [SerializeField] TextMeshProUGUI _authorName;
    [SerializeField] Image _authorImg;
    [SerializeField] Image[] _ratingImages;
    [SerializeField] Image[] _friendImages;
    [SerializeField] Image _coverImg;

    [SerializeField] Sprite _filledStar; 
    [SerializeField] Sprite _emptyStar;

    [SerializeField] BookLocalContainer _bookDataDefault;

    void Start()
    {
        UpdateUI(_bookDataDefault.Books[0]);    
    }

    public void UpdateUI(BookData book)
    {
        _title.text = book.Title;
        _description.text = book.Description;
        _authorName.text = book.Authors[0];
        //_authorImg.sprite
        int stars = Mathf.RoundToInt(book.Rating.x);
        
        for (int i = 0; i < book.Rating.y; i++)
        {
            _ratingImages[i].sprite = i <= stars ? _filledStar : _emptyStar;
        }

        _coverImg.sprite = book.Cover;
        if (UserManager.Instance.CurrentUser.Friends.Count == 0) return;

        UserData[] readerFriends = UserManager.Instance.CurrentUser.Friends.
            Where(f => f.OwnedBooks.Any(b => b.BookData.OLID == book.OLID))
            .Take(_friendImages.Length).ToArray();

        for (int i = 0; i < _friendImages.Length; i++)
        {
            _friendImages[i].sprite = readerFriends[i].ProfilePicture;
        }
    }
}
