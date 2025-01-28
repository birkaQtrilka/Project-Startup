using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class LibraryManager : MonoBehaviour
{
    [SerializeField] int _itemsPerRow; 
    [SerializeField] GameObject _itemPrefab;
    [SerializeField] Transform _rowContainerPrefab;
    [SerializeField] Transform _libraryContainer;
    [SerializeField] bool _updateOnEnable;
    [SerializeField] LibraryListProvider _libraryListProvider;
    [SerializeField] int _showListAmount = 3;

    public void AddList(OwnedBook book)
    {
        _libraryListProvider.AddToLastList(book);
    }

    public void RemoveList(OwnedBook book)
    {
        _libraryListProvider.RemoveFromLastList(book);
    }

    public bool IsInList(OwnedBook book)
    {
        return _libraryListProvider.IsInLastList(book);
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

        int count = Mathf.Min(lists.Count, _showListAmount);

        for (int j = 0; j < count; j++)
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
                }
                TextMeshProUGUI header = wrapper.GetComponentInChildren<TextMeshProUGUI>();
                if (header != null)
                    header.text = list.Name;

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
