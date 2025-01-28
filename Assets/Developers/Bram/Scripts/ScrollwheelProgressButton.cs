using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrollwheelProgressButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] int number;
    [SerializeField] ProgressScrollwheelSelector selector;

    public void SetValues(int pPageNumber, ProgressScrollwheelSelector pSelector)
    {
        number = pPageNumber;
        selector = pSelector;
        text.text = number.ToString();
    }

    public void Select()
    {
        selector.SetCurrentPage(number);
    }
}
