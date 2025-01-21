using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookDataUI : MonoBehaviour
{

    //[SerializeField] SearchBar _bookGetter;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Image _bookImage;
    [SerializeField] Button _leftButton;
    [SerializeField] Button _rightButton;
    [SerializeField] Button _ownBookButton;
    [SerializeField] Button _disownBookButton;
    [SerializeField] UserData _user;

    int _pos;
    BookData[] _books;

    void OnEnable()
    {
        _disownBookButton.onClick.AddListener(DisownBook);
        _ownBookButton.onClick.AddListener(OwnBook);
        _leftButton.onClick.AddListener(DecrementPos);
        _rightButton.onClick.AddListener(IncrementPos);
    }

    void OnDisable()
    {
        _disownBookButton.onClick.RemoveListener(DisownBook);
        _ownBookButton.onClick.RemoveListener(OwnBook);
        _leftButton.onClick.RemoveListener(DecrementPos);
        _rightButton.onClick.RemoveListener(IncrementPos);
    }

    public void OwnBook()
    {
        if (_books == null || _books.Length == 0 || _books[_pos] == null) return;

        _user.OwnABook(_books[_pos]);
        _user.ClickedBookIds.Add(_books[_pos].Title);
    }

    public void DisownBook()
    {
        if (_books == null || _books.Length == 0 || _books[_pos] == null) return;

        _user.DisownBook(_books[_pos]);
    }

    public void SetBooks(BookData[] books)
    {
        _books = books;
        UpdateUI();
    }

    public void IncrementPos()
    {
        if (_books == null || _books.Length < _pos + 2) return;
        _pos++; 
        UpdateUI();
    }

    public void DecrementPos()
    {
        if (_books == null || _pos <= 0) return;

        _pos--;
        UpdateUI();
    }

    public void UpdateUI()
    {
        _text.text = _books[_pos]?.ToString();
        _bookImage.sprite = _books[_pos]?.Cover;
    }
}
