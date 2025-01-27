using UnityEngine;
using UnityEngine.UI;

public class ActiveButtonUI : MonoBehaviour
{
    [SerializeField] Image[] _buttons;
    [SerializeField] Color _activeColor;
    [SerializeField] Color _inactiveColor;

    void Start()
    {
        foreach (var button in _buttons)
        {
            var obj = button.gameObject;
            button.GetComponent<Button>().onClick.AddListener(() => SelectButton(obj));
        }
    }

    public void SelectButton(GameObject buttonObj)
    {
        Image buttonImg = buttonObj.GetComponent<Image>();

        foreach (var btn in _buttons)
        {
            if (btn == buttonImg) btn.color = _activeColor;
            else btn.color = _inactiveColor;
        }
    }

}
