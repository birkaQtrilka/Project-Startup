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
    [SerializeField] Button _trackProgressButton;

    [SerializeField] Sprite _filledStar;
    [SerializeField] Sprite _emptyStar;

    [SerializeField] BookLocalContainer _bookDataContainer;
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
    public BookDataSO _bookData;


    void Start()
    {
        UpdateUI(_bookData);
        
    }

    public void UpdateUI(BookDataSO book)
    {
        _bookData = book;
        
        if (_trackProgressButton != null)
        {
            bool isOwnerOfBook = UserManager.Instance.CurrentUser.OwnedBooks.Any(b => b.BookData == _bookData);

            _trackProgressButton.gameObject.SetActive(isOwnerOfBook);
        }

        if (_title != null)
            _title.text = book.Title;
        if (_description != null)
            _description.text = book.Description;
        if (_longDescription != null)
            _longDescription.text = book.Description;
        if(_authorName != null) 
            _authorName.text = book.Authors[0];
        //_authorImg.sprite

        StefUtils.SetRating(book.Rating.x, StefUtils.MAX_RATING, _ratingImages,_filledStar, _emptyStar);

        if(_coverImg != null) 
            _coverImg.sprite = book.Cover;

        if(_getBookButton != null)
        {
            _getBookButton.onClick.RemoveAllListeners();
            if(_linkToAmazon)
                _getBookButton.onClick.AddListener(() => {
                    var copy = book;
                    Application.OpenURL("https://www.amazon.com/dp/" + copy.Isbn);
                });
            else
                _getBookButton.onClick.AddListener(() => {
                    var copy = book;
                    Application.OpenURL(book.OpenLibraryLink);
                });

        }

        if (_ageRating != null)
            _ageRating.text = "13+";
        if (_genre != null)
            _genre.text = book.Genres?.GetRandomItem();
        if (_language != null)
            _language.text = "English";

        SetReviews(_bookData);
        SetNotes(_bookData);
        SetFriends(_bookData);
       

    }

    void SetFriends(BookDataSO book)
    {
        for (int i = 0; i < _friendImages.Length; i++)
        {
            _friendImages[i].gameObject.SetActive(false);
        }

        if (UserManager.Instance.CurrentUser.Friends.Count == 0)
        {
            return;
        }

        UserData[] readerFriends = UserManager.Instance.CurrentUser.Friends.
            Where(f => f.OwnedBooks.Any(b => b.BookData == book))
            .Take(_friendImages.Length).ToArray();

        int count = Mathf.Min(_friendImages.Length, readerFriends.Length);

        for (int i = 0; i < count; i++)
        {

            _friendImages[i].gameObject.SetActive(true);
            _friendImages[i].sprite = readerFriends[i].ProfilePicture;
        }
    }

    void SetNotes(BookDataSO book)
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

    void SetReviews(BookDataSO book)
    {
        //local average
        float average = book.LocalReviews.Count == 0 ? 0 :(float)book.LocalReviews.Average((r) => r.Rating);

        StefUtils.SetRating(average, StefUtils.MAX_RATING, _localRatingImages, _filledStar, _emptyStar);


        if (_reviewsCount != null)
            _reviewsCount.text = book.LocalReviews.Count.ToString();

        if (_reviewContainer == null) return;


        int numChildren = _reviewContainer.childCount;
        for (int i = numChildren - 1; i >= 0; i--)
        {
            GameObject.Destroy(_reviewContainer.GetChild(i).gameObject);
        }

        for (int i = 0; i < book.LocalReviews.Count; i++)
        {
            ReviewData reviewData = book.LocalReviews[i];

            ReviewUI review = Instantiate(_reviewPrefab, _reviewContainer.transform);
            review.UpdateUI(reviewData);
        }
    }
}
