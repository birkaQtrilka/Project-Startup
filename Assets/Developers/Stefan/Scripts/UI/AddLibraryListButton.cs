using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class AddLibraryListButton : MonoBehaviour
{
    [SerializeField] LibraryListProvider _libraryListProvider;
    [SerializeField] PageManager _libraryTabsManager;
    [SerializeField] Page _addBooksToListPage;
    [SerializeField] GameObject _overlayContainer;

    Button _btn;
    TMP_InputField _inputField;

    void Awake()
    {
        _btn = GetComponent<Button>();
        _inputField = GetComponent<TMP_InputField>();
    }

    void OnEnable()
    {
        _btn.onClick.AddListener(OnClick);
    }

    void OnDisable()
    {
        _btn.onClick.RemoveListener(OnClick);

    }

    void OnClick()
    {
        _overlayContainer.SetActive(false);

        _libraryListProvider.AddList(_inputField.text);
        _libraryTabsManager.SwitchToPage(_addBooksToListPage);
    }
}
