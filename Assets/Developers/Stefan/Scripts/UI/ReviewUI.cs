using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviewUI : MonoBehaviour
{
    [SerializeField] Sprite _filledStar;
    [SerializeField] Sprite _emptyStar;
    [Space]
    [SerializeField] TextMeshProUGUI _publishTime;
    [SerializeField] TextMeshProUGUI _content;
    [SerializeField] TextMeshProUGUI _userName;
    [SerializeField] TextMeshProUGUI _userReviewsCount;
    [SerializeField] Image _userPFP;

    [SerializeField] Image[] _localRatingImages;
    public void UpdateUI(ReviewData data)
    {
        _publishTime.text = data.PublishTime;
        _content.text = data.Content;
        _userName.text = data.UserData.NickName;
        _userReviewsCount.text = data.UserData.Posts.Count.ToString();
        _userPFP.sprite = data.UserData.ProfilePicture;

        SetRating(data);
    }

    void SetRating(ReviewData data)
    {
        int stars = Mathf.RoundToInt(data.Rating);
        if (_localRatingImages != null && _localRatingImages.Length != 0)
            for (int i = 0; i < StefUtils.MAX_RATING; i++)
            {
                _localRatingImages[i].sprite = i <= stars ? _filledStar : _emptyStar;
            }
    }
}
