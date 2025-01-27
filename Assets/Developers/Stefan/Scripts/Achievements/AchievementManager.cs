using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "AchievementManager")]
public class AchievementManager : ScriptableObject
{
    public UnityEvent<Achievement> OnAchievementCompleted;

    [SerializeField] List<Achievement> _available;

    [SerializeField] List<Achievement> _completedAchievement;

    public void CheckForAchievement(OwnedBook bookData)
    {
        Achievement achievement = FindFirstAvailableAchievement(bookData);
        if (achievement == null) return;
        Debug.Log("Completed achievement: " + achievement.Name);
        _completedAchievement.Add(achievement);
        _available.Remove(achievement);
    }

    Achievement FindFirstAvailableAchievement(OwnedBook bookData)
    {

        foreach (var a in _available)
        {
           if(a.Conditions.All(c=>c.CanApply(bookData)))
                return a;
        }
        return null;
    }
}
