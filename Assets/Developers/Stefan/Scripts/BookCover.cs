using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BookCover : MonoBehaviour
{
    public event Action OnBookUpdate;

    static PageManager _rootPageManager;

    [SerializeField] Image _image;
    [SerializeField] BookLocalContainer _container;
    [SerializeField] string _bookOLID;
    
    public BookLocalContainer Container => _container;
    public BookData BookData { get; private set; }

    public void SendToBookOverview()
    {
        if(_rootPageManager == null)
            _rootPageManager = GameObject.FindFirstObjectByType<Canvas>().GetComponent<PageManager>();

        Page page = _rootPageManager.SwitchToPageAndGet("DetailedBookPage");
        DetailedBookUI bookUI = page.GetComponentInChildren<DetailedBookUI>();
        
        BookData ??= _container.Books.First(b => b.OLID == _bookOLID);

        if(BookData != null )
            bookUI.UpdateUI(BookData);
    }

    void OnEnable()
    {
        if(BookData == null)
            UpdateUI();
    }

    public void UpdateUI()
    {
        BookData = _container.Books.FirstOrDefault(b => b.OLID == _bookOLID);
        
        // for BookTitleDisplayer to update text
        OnBookUpdate?.Invoke();

        if (BookData == null) return;

        _image.sprite = BookData.Cover;
    }
}
