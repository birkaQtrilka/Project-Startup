using UnityEngine;
using UnityEngine.UI;

public class ActiveButtonUI : MonoBehaviour
{
    [SerializeField] Image[] _buttons;
    [SerializeField] Color _activeColor;
    [SerializeField] Color _inactiveColor;
    [SerializeField] bool _doImageSwap;
    [SerializeField] Sprite _activeImage;
    [SerializeField] Sprite _inactiveImage;

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
            if (_doImageSwap)
            {
                btn.sprite = btn == buttonImg ? _activeImage : _inactiveImage;
            }
            else
            {
                btn.color = btn == buttonImg ? _activeColor : _inactiveColor;
            }
        }
    }

}
