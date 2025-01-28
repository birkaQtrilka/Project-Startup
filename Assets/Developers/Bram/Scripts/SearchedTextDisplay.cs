using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SearchedTextDisplay : MonoBehaviour
{

    public void SearchResult(TMP_InputField pInput)
    {
        GetComponent<TextMeshProUGUI>().text = "You searched for: " + pInput.text;
        pInput.text = "";
    }


}
