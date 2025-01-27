using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatMessage : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI messageText;

    private void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent.GetComponent<RectTransform>());
    }


    public void SetText(string _text)
    {
        messageText.text = _text;
    }
}
