using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class ActiveButton : MonoBehaviour
{
    [SerializeField] Sprite _active;
    [SerializeField] Sprite _inactive;

    Image _image;

    void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetActive()
    {
        _image.sprite = _active;
    }

    public void SetInactive()
    {
        _image.sprite = _inactive;

    }
}
