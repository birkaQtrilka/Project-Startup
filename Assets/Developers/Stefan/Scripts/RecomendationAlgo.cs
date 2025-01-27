using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(menuName = "RecomendationAlgorithm")]
public class RecomendationAlgo : ScriptableObject
{
    [SerializeField] UserData _mainUser;
    [SerializeField] int _keywordCount;
    [SerializeField] int _seed;
    Random _random;

    List<List<string>> _keywordSources = new(3);


    void Awake()
    {
        _random = new Random(_seed);
    }

    void OnEnable()
    {
        _keywordSources.Add(_mainUser.SearchedStuff);
        _keywordSources.Add(_mainUser.OwnedBooks.Select(b => b.BookData.Authors[0]).ToList());
        _keywordSources.Add(_mainUser.ClickedBookIds);
    }

    public string GetQuerry()
    {
        _random ??= new Random(_seed);

        string[] keywords = new string[_keywordCount];
        int count;
        if(_keywordCount > _keywordSources.Count ) count = _keywordSources.Count;
        else count = _keywordCount;

        for (int i = 0; i < count; i++)
        {
            string randomKeyword = _keywordSources.
                Where(source => source.Count != 0).ToList().
                GetRandomItem(_random)?.
                GetRandomItem(_random)?.
                Split(' ')?.
                GetRandomItem(_random);
            keywords[i] = randomKeyword;

        }
        return string.Join(" ", keywords);
    }

    
}
