using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
public static class StefUtils
{
    public const int MAX_RATING = 5;
    static Random _defaultRandom = new Random();
    public static T GetRandomItem<T>(this List<T> list, Random randomGen = null)
    {
        _defaultRandom ??= new();
        if (randomGen == null) randomGen = _defaultRandom;
        if (list.Count == 0) return default;
        return list[randomGen.Next(0, list.Count)];
    }

    public static T GetRandomItem<T>(this T[] list, Random randomGen = null)
    {
        _defaultRandom ??= new();
        if (randomGen == null) randomGen = _defaultRandom;
        if (list.Length == 0) return default;

        return list[randomGen.Next(0, list.Length)];
    }
    
    public static string GetRandomItem(this string[] list, Random randomGen = null)
    {
        _defaultRandom ??= new();
        if (randomGen == null) randomGen = _defaultRandom;

        if (list.Length == 0) return default;
        
        return list[randomGen.Next(0, list.Length)];
    }

    public static void DestroyAllChildren(this Transform container)
    {
        int numChildren = container.childCount;
        for (int i = numChildren - 1; i >= 0; i--)
        {
            GameObject.Destroy(container.GetChild(i).gameObject);
        }
    }

    public static void InstantiateMultiple<T>(this Transform container, T prefab, int count, Action<T, int> OnInstance = null) where T : MonoBehaviour
    {
        for (int i = 0; i < count; i++)
        {
            T instance = GameObject.Instantiate(prefab, container);

            OnInstance?.Invoke(instance, i);
        }
    }

    public static Transform FindDeepChild(this Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;
            var result = child.FindDeepChild(name);
            if (result != null)
                return result;
        }
        return null;
    }

    public static void SetRating(float average, int max, Image[] images, Sprite filledStar, Sprite emptyStar)
    {
        int stars = Mathf.FloorToInt(average);
        if (images != null && images.Length != 0)
            for (int i = 0; i < max; i++)
            {
                images[i].sprite = emptyStar;
                Image fillImage = images[i].transform.GetChild(0).GetComponent<Image>();
                if (i >= stars)
                {
                    fillImage.sprite = filledStar;
                    fillImage.type = Image.Type.Filled;
                    fillImage.fillMethod = Image.FillMethod.Horizontal;
                    fillImage.fillAmount = Mathf.Max(0, average - i);
                }
            }
    }
}
