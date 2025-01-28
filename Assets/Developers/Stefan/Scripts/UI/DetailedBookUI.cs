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
    [SerializeField] Button _getBookButton;
    [SerializeField] Image _authorImg;
    [SerializeField] Image[] _ratingImages;
    [SerializeField] Image[] _friendImages;
    [SerializeField] Image _coverImg;

    [SerializeField] Sprite _filledStar;
    [SerializeField] Sprite _emptyStar;

    [SerializeField] BookLocalContainer _bookDataContainer;
    [SerializeField] string _bookOLID;
    [Header("For Info")]
    [SerializeField] TextMeshProUGUI _ageRating;
    [SerializeField] TextMeshProUGUI _longDescription;
    [SerializeField] TextMeshProUGUI _genre;
    [SerializeField] TextMeshProUGUI _language;
    [Header("For Notes")]
    [SerializeField] NoteUI _notePrefab;
    [SerializeField] Transform _noteContainer;
    [Header("For local reviews")]
    [SerializeField] TextMeshProUGUI _reviewsCount;
    [SerializeField] Image[] _localRatingImages;
    [SerializeField] ReviewUI _reviewPrefab;
    [SerializeField] Transform _reviewContainer;
    [Header("If false, will link to open library page")]
    [SerializeField] bool _linkToAmazon;

    void Start()
    {
        BookData book = _bookDataContainer.Books.FirstOrDefault(b => b.OLID == _bookOLID) ?? _bookDataContainer.Books[0];
        UpdateUI( book );    
    }

    public void UpdateUI(BookData book)
    {
        if (_title != null)
            _title.text = book.Title;
        if (_description != null)
            _description.text = book.Description;
        if (_longDescription != null)
            _longDescription.text = book.Description;
        if(_authorName != null) 
            _authorName.text = book.Authors[0];
        //_authorImg.sprite

        SetRating(book.Rating.x, StefUtils.MAX_RATING, _ratingImages);

        if(_coverImg != null) 
            _coverImg.sprite = book.Cover;

        if(_getBookButton != null)
        {
            _getBookButton.onClick.RemoveAllListeners();
            if(_linkToAmazon)
                _getBookButton.onClick.AddListener(() => Application.OpenURL("https://www.amazon.com/dp/" + book.Isbn));
            else
                _getBookButton.onClick.AddListener(() => Application.OpenURL(book.OpenLibraryLink));

        }

        if (_ageRating != null)
            _ageRating.text = "13+";
        if (_genre != null)
            _genre.text = book.Genres?.GetRandomItem();
        if (_language != null)
            _language.text = "English";

        SetReviews( book );
        SetNotes( book );
        SetFriends( book );
       

    }

    void SetRating(float average, int max, Image[] images)
    {
        int stars = Mathf.RoundToInt(average);
        if (images != null && images.Length != 0)
            for (int i = 0; i < max; i++)
            {
                images[i].sprite = i <= stars ? _filledStar : _emptyStar;
            }
    }

    void SetFriends(BookData book)
    {
        if (UserManager.Instance.CurrentUser.Friends.Count == 0)
        {
            for (int i = 0; i < _friendImages.Length; i++)
            {
                _friendImages[i].gameObject.SetActive(false);
            }
            return;
        }

        UserData[] readerFriends = UserManager.Instance.CurrentUser.Friends.
            Where(f => f.OwnedBooks.Any(b => b.BookData.OLID == book.OLID))
            .Take(_friendImages.Length).ToArray();

        int count = Mathf.Min(_friendImages.Length, readerFriends.Length);

        for (int i = 0; i < count; i++)
        {

            _friendImages[i].gameObject.SetActive(true);
            _friendImages[i].sprite = readerFriends[i].ProfilePicture;
        }
    }

    void SetNotes(BookData book)
    {
        if (_noteContainer == null) return;

        int numChildren = _noteContainer.childCount;
        for (int i = numChildren - 1; i >= 0; i--)
        {
            GameObject.Destroy(_noteContainer.GetChild(i).gameObject);
        }
        for (int i = 0; i < book.Notes.Count; i++)
        {
            PostData noteData = book.Notes[i];

            NoteUI noteUI = Instantiate(_notePrefab, _noteContainer.transform);
            noteUI.Init(noteData);
        }
    }

    void SetReviews(BookData book)
    {
        //local average
        float average = (float)book.LocalReviews.Average((r) => r.Rating);

        SetRating(average, StefUtils.MAX_RATING, _localRatingImages);



        if (_reviewsCount != null)
            _reviewsCount.text = book.LocalReviews.Count.ToString();

        if (_reviewContainer == null) return;


        int numChildren = _reviewContainer.childCount;
        for (int i = numChildren - 1; i >= 0; i--)
        {
            GameObject.Destroy(_reviewContainer.GetChild(i).gameObject);
        }
        for (int i = 0; i < book.Notes.Count; i++)
        {
            ReviewData reviewData = book.LocalReviews[i];

            ReviewUI review = Instantiate(_reviewPrefab, _reviewContainer.transform);
            review.UpdateUI(reviewData);
        }
    }
}
