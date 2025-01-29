using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class FontReplacer : MonoBehaviour
{

    [SerializeField] TMP_FontAsset _fontAsset;
    [SerializeField] bool _execute;

    void Update()
    {
        if(_execute)
        {
            _execute = false;
            if (_fontAsset == null) return;

            var textObjects = GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (var textObject in textObjects)
            {
                textObject.font = _fontAsset;
            }
        }
    }
}
