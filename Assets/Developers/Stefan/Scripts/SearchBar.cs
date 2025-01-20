using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SearchBar : MonoBehaviour
{
    [SerializeField] string _defaultInput = "english";
    [SerializeField] BookGetter _bookGetter;
    [SerializeField] TMP_InputField _inputField;
    [SerializeField] Button _button;
    [SerializeField] BookDataUI _bookDataUI;

    void OnEnable()
    {
        _button.onClick.AddListener(OnValueEnter); 
    }

    void OnDisable()
    {
        _button.onClick.RemoveListener(OnValueEnter);

    }

    public void OnValueEnter()
    {
        _ = UpdateAfterGet();
    }

    public async Task UpdateAfterGet()
    {
        string input = string.IsNullOrEmpty(_inputField.text) ? _defaultInput : _inputField.text;
        Debug.Log("searching input: " + input);

        var books = await Search(input, 5);
        Debug.Log("Setting UI");
        _bookDataUI.SetBooks(books);
    }

    public async Task<BookData[]> Search(string input, int amount)
    {
        return await _bookGetter.FetchData(amount, input);
    }
}
