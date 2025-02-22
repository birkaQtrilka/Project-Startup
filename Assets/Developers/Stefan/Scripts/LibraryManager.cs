using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class LibraryManager : MonoBehaviour
{
    public static string CurrentList;

    [SerializeField] int _itemsPerRow; 
    [SerializeField] GameObject _itemPrefab;
    [SerializeField] Transform _rowContainerPrefab;
    [SerializeField] Transform _libraryContainer;
    [SerializeField] bool _updateOnEnable;
    [SerializeField] LibraryListProvider _libraryListProvider;
    [SerializeField] int _showListAmount = 3;
    [SerializeField] int _indexOffset;
    [SerializeField] Button _editButtonPrefab;
    [SerializeField] PageManager _libraryPageManager;
    [SerializeField] Page _editListPage;

    public void AddList(OwnedBook book)
    {
        _libraryListProvider.AddToList(CurrentList, book);
    }

    public void RemoveList(OwnedBook book)
    {
        _libraryListProvider.RemoveFromList(CurrentList, book);
    }

    public bool IsInList(OwnedBook book)
    {
        return _libraryListProvider.IsInList(CurrentList, book);
    }

    public void SetCurrentList(string name)
    {
        CurrentList = name;
    }

    void OnEnable()
    {

        StartCoroutine(WaitAFrame());
    }

    IEnumerator WaitAFrame()
    {
        yield return null;
        if (_updateOnEnable)
        {
            UpdateUI();
        }
    }


    public void UpdateUI()
    {
        _libraryContainer.DestroyAllChildren();

        Transform currentRow = null;
        Transform wrapper = null;
        var lists = _libraryListProvider.GetList();

        int count = Mathf.Min(lists.Count, _showListAmount + _indexOffset);

        for (int j = _indexOffset; j < count; j++)
        {
            var list = lists[j];
            int i = 0;
            foreach (var ownedBook in list.OwnedBooks)
            {
                if (i % _itemsPerRow == 0)
                {
                    wrapper = Instantiate(_rowContainerPrefab, _libraryContainer);
                    currentRow = wrapper.FindDeepChild("row");
                    currentRow = currentRow == null ? wrapper : currentRow;

                    TextMeshProUGUI header = wrapper.GetComponentInChildren<TextMeshProUGUI>();
                    if (header != null)
                    {
                        header.text = list.Name;
                        Button editButton = Instantiate(_editButtonPrefab, header.transform);
                        int copy = j;
                        //RectTransform buttonRectTransf = editButton.GetComponent<RectTransform>();
                        //buttonRectTransf.anchoredPosition = Vector2.right * (buttonRectTransf.rect.width + buttonRectTransf.anchoredPosition.x);
                        
                        editButton.onClick.AddListener(() =>
                        {
                            _editListPage.GetComponent<LibraryManager>()._indexOffset = copy;
                            LibraryManager.CurrentList = list.Name;
                            _libraryPageManager.SwitchToPage(_editListPage);
                        });
                    }
                }
                

                var inst = Instantiate(_itemPrefab, currentRow);
                if (inst.TryGetComponent(out BookCover cover))
                {
                    cover.BookData = ownedBook.BookData;
                    cover.UpdateUI();
                }

                if (inst.TryGetComponent(out LibraryItem libraryItem))
                {
                    libraryItem.Init(this, ownedBook);

                }

                i++;
            }
        }
        

        
    }
}
