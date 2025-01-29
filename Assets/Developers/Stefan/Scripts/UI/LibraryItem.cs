using UnityEngine;


[RequireComponent(typeof(BookCover))]
public class LibraryItem : MonoBehaviour
{
    public bool Interactable = true;

    [SerializeField] GameObject _activeState;
    [SerializeField] GameObject _inactiveState;

    BookCover _cover;
    LibraryManager _manager;
    OwnedBook _book;

    bool _selected;

    void Awake()
    {
        _cover = GetComponent<BookCover>();
        
    }

    public void Init(LibraryManager manager, OwnedBook book)
    {
        _manager = manager;
        _book = book;
        _selected = IsInSelectedList();

        _activeState.SetActive(false);
        _inactiveState.SetActive(false);

        if (!Interactable) return;

        if (_selected)
            SetSelected();
        else
            SetDeselected();
    }

    public void SetSelected()
    {
        _activeState.SetActive(false);
        _inactiveState.SetActive(true);
    }

    public void SetDeselected()
    {
        _activeState.SetActive(true);
        _inactiveState.SetActive(false);
    }

    public void Toggle()
    {
        if (!Interactable) return;

        _selected = !_selected;
        if (_selected)
            AddToList();
        else
            RemoveFromList();
    }

    public void AddToList()
    {
        SetSelected();
        _manager.AddList(_book);
    }

    public void RemoveFromList() 
    {
        SetDeselected();
        _manager.RemoveList(_book);

    }

    bool IsInSelectedList()
    {
        return _manager.IsInList(_book);
    }
}
