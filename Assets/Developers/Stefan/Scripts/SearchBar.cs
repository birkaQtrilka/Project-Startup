using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class SearchBar : MonoBehaviour
{
    public UnityEvent<string> OnSearch;

    [SerializeField] string _defaultInput = "english";
    [SerializeField] BookGetter _bookGetter;
    [SerializeField] RecomendationAlgo _recomendationAlgo;
    [SerializeField] TMP_InputField _inputField;
    [SerializeField] Button _button;
    [SerializeField] BookDataUI _bookDataUI;

    [SerializeField] bool _searchOnStart = false;

    Task<BookData[]> _searchProcess;


    void Start()
    {
        if (!_searchOnStart) return;
        string querry = _recomendationAlgo.GetQuerry();

        _ = UpdateAfterGet(querry);

    }

    void OnEnable()
    {
        _button.onClick.AddListener(OnValueEnter); 
    }

    void OnDisable()
    {
        _button.onClick.RemoveListener(OnValueEnter);
        _bookGetter.CancelGetting();
    }

    public void OnValueEnter()
    {
        _ = UpdateAfterGet();
    }

    public async Task UpdateAfterGet()
    {
        string input = string.IsNullOrEmpty(_inputField.text) ? _defaultInput : _inputField.text;
        await UpdateAfterGet(input);
    }

    public async Task UpdateAfterGet(string input)
    {
        input = string.IsNullOrEmpty(input.Trim(' ', '\n')) ? _defaultInput : input;

        if (_searchProcess != null && !_searchProcess.IsCompleted)
        {
            Debug.Log("Can't search because I'm in the process");
            return;
        }

        Debug.Log("searching input: " + input);
        OnSearch?.Invoke(input);
        _searchProcess = Search(input, 5);
        var books = await _searchProcess;

        Debug.Log("Setting UI");
        _bookDataUI.SetBooks(books);
        _searchProcess= null;
    }
    async Task<BookData[]> Search(string input, int amount)
    {

        return await _bookGetter.FetchData(amount, input);
    }
}
