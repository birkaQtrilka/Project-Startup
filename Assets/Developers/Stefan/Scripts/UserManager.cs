using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserManager : MonoBehaviour
{
    public static UserManager Instance { get; private set; }
    GameObject _lastSelectedButton;

    [field: SerializeField] public UserData CurrentUser { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(_lastSelectedButton);
        _lastSelectedButton = EventSystem.current.currentSelectedGameObject;
    }
}
