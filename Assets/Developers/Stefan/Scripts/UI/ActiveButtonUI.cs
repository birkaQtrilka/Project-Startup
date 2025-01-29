using UnityEngine;
using UnityEngine.UI;

public class ActiveButtonUI : MonoBehaviour
{
    [SerializeField] ActiveButton[] _buttons;

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
        ActiveButton buttonImg = buttonObj.GetComponent<ActiveButton>();

        foreach (var btn in _buttons)
        {
            if(btn == buttonImg)
            {
                btn.SetActive();
            }
            else
            {
                btn.SetInactive();
            }
        }
    }

}
