using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    
    //public static ChatManager Instance { get; private set; }

    [SerializeField] ChatMessage _chatMessagePrefab;
    [SerializeField] CanvasGroup _chatContent;
    [SerializeField] TMP_InputField _chatInput;

    /*private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else
        {
            Destroy(gameObject);
        }
    }*/

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendChatMessage();
        }
    }

    public void SendChatMessage()
    {
        SendChatMessage(_chatInput.text);
        _chatInput.text = "";
    }

    public void SendChatMessage(string pMessage)
    {
        if (string.IsNullOrEmpty(pMessage.Trim('\n',' '))) return;

        // Can add some formatting here
        string s = pMessage;

        AddMessage(s);
    }


    void AddMessage(string pMsg)
    {
        ChatMessage CM = Instantiate(_chatMessagePrefab, _chatContent.transform);
        CM.SetText(pMsg);

    }



}
