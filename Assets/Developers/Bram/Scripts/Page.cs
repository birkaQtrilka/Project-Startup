using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{

    [SerializeField] 
    private string _identifier;
    
    public string GetIdentifier()
    {
        return _identifier.ToLower();
    }

}
