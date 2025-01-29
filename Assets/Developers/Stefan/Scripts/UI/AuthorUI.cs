using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AuthorUI : MonoBehaviour
{
    [SerializeField] Image _portrait;
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _description;
    //[SerializeField] Button _checkOutBtn;

    [SerializeField] AuthorData _authorData;

    void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        _portrait.sprite = _authorData.Image;
        _name.text = _authorData.Name;
        _description.text = _authorData.Description;
    }
}
