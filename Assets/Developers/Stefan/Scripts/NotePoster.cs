using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System;

public class NotePoster : MonoBehaviour
{
    [SerializeField] TMP_InputField _noteContent;
    [SerializeField] TMP_InputField _bookSelectInput;
    [SerializeField] TMP_InputField _pageSelectInput;
    [SerializeField] Transform _searchResultsContainer;
    [SerializeField] Transform _searchResultsContainerWrapper;
    [SerializeField] Button _submitBtn;
    [SerializeField] Button _cancelBtn;
    [SerializeField] Image _chosenBookImage;
    [SerializeField] BookLocalContainer _container;
    [SerializeField] PostListUI _postListUI;

    [Header("Assets")]
    [SerializeField] Button _searchResultPrefab;

    OwnedBook _currentChosenBook;

    void OnEnable()
    {
        _bookSelectInput.onSelect.AddListener(OnBookInputSelect);   
        //_bookSelectInput.onDeselect.AddListener(OnBookInputDeselect);
        _bookSelectInput.onValueChanged.AddListener(OnBookInputValueChanged);

        _pageSelectInput.onValueChanged.AddListener(OnPageInputValueChanged);
        _submitBtn.onClick.AddListener(OnSubmitPress);
        _cancelBtn.onClick.AddListener(OnBookInputDeselect);

    }

    void OnDisable()
    {
        _bookSelectInput.onSelect.RemoveListener(OnBookInputSelect);
        //_bookSelectInput.onDeselect.RemoveListener(OnBookInputDeselect);
        _bookSelectInput.onValueChanged.RemoveListener(OnBookInputValueChanged);

        _pageSelectInput.onValueChanged.RemoveListener(OnPageInputValueChanged);
        _submitBtn.onClick.RemoveListener(OnSubmitPress);

    }

    void OnSubmitPress()
    {
        if (_currentChosenBook == null) return;

        UserData currentUser = UserManager.Instance.CurrentUser;

        currentUser.Posts.Add(new PostData() { 
            Content = _noteContent.text,
            IsEdited = false,
            OLID = _currentChosenBook.BookData.OLID,
            Page = int.Parse(_pageSelectInput.text),
            PublishTime = " 1 minute ago",
            UserData = currentUser,
            ID = Mathf.FloorToInt(Time.time * 100 * UnityEngine.Random.value),
            });

        _noteContent.text = "";

        _container.IncludeNotesInBooks();
    }

    void OnBookInputSelect(string str)
    {
        _searchResultsContainerWrapper.gameObject.SetActive(true);
        OnBookInputValueChanged(str);

    }

    void OnBookInputDeselect()
    {
        _searchResultsContainerWrapper.gameObject.SetActive(false);

    }

    void OnPageInputValueChanged(string str)
    {
        if (_currentChosenBook == null)
        {
            _pageSelectInput.text = "";
            return;
        }
        if (str.Length == 0) return;

        if(!char.IsNumber(str[^1]))
        {
            _pageSelectInput.text = str[..^2];
        }
    }

    void OnBookChosen(OwnedBook book)
    {
        _currentChosenBook = book;

        _chosenBookImage.sprite = book.BookData.Cover;
        OnBookInputDeselect();
        _bookSelectInput.text = book.BookData.Title;

        
        _postListUI.UpdateUI(book.BookData.Notes);

    }

    void OnBookInputValueChanged(string value)
    {
        var searchResult = UserManager.Instance.CurrentUser.OwnedBooks.Where(b => b.BookData.Title.StartsWith(value)).ToList();

        _searchResultsContainer.DestroyAllChildren();

        _searchResultsContainer.InstantiateMultiple(_searchResultPrefab, searchResult.Count, (inst, i) =>
        {
            inst.transform.Find("Cover").GetComponent<Image>().sprite = searchResult[i].BookData.Cover;
            inst.GetComponentInChildren<TextMeshProUGUI>().text = searchResult[i].BookData.Title;

            OwnedBook bookReferenceCopy = searchResult[i];
            inst.onClick.AddListener(()=> OnBookChosen(bookReferenceCopy));
        });
    }
}
