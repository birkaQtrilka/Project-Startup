using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class RandomBooks : MonoBehaviour
{
    [SerializeField] bool _execute;

    void Update()
    {
        if(_execute)
        {
            _execute = false;

            var covers = GetComponentsInChildren<BookCover>();
            BookDataSO[] books = Resources.LoadAll<BookDataSO>("");

            int count = Mathf.Min(covers.Length, books.Length);

            for (int i = 0; i < count; i++)
            {
                covers[i].BookData = books[i];

#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(covers[i]);
#endif
            }
        }
    }

    
}
