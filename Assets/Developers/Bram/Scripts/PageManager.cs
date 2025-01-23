using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    // Store info
    private Page[] _pages;
    private Page _currentPage;

    private Stack<Page> _pageHistory;

    /*// Singleton
    public static PageManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }*/

    private void Start()
    {
        _pages = GetComponentsInChildren<Page>(true);
        foreach (Page pPage in _pages)
        {
            if (pPage.transform.parent == transform) pPage.gameObject.SetActive(false);
        }

        /*for (int i = 0; i < _pages.Length; i++)
        {
            _pages[i].gameObject.SetActive(false);
        }*/
        _currentPage = _pages[0];
        _currentPage.gameObject.SetActive(true);
        _pageHistory = new Stack<Page>();
    }

    // Page navigation
    void SetCurrentPage(Page pNewPage)
    {
        if (pNewPage == null)
        {
            Debug.Log("Page is invalid, do a double check if the identifier or index is right");
            return;
        }
        _currentPage.gameObject.SetActive(false);
        _currentPage = pNewPage;
        _currentPage.gameObject.SetActive(true);
    }

    // Functions for the buttons
    public Page SwitchToPageAndGet(string pPageIdentifier)
    {
        Page page = null;
        for (int i = 0; i < _pages.Length; i++)
        {
            if (_pages[i].GetIdentifier() == pPageIdentifier.ToLower() &&
                _pages[i].transform.parent == transform) page = _pages[i];
        }
        if (page != null) SwitchToPage(page);

        return page;
    }
    public void SwitchToPage(string pPageIdentifier)
    {
        SwitchToPageAndGet(pPageIdentifier);
    }

    public void SwitchToPage(Page pPage)
    {
        _pageHistory.Push(_currentPage);
        SetCurrentPage(pPage);
    }

    public void Back()
    {
        if (_pageHistory.Count > 0)
        {
            SetCurrentPage(_pageHistory.Pop());
        }
    }
}
