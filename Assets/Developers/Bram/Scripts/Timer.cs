using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] bool _isActive;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] float _remainingTime;
    [SerializeField] GameObject[] _timeControlButtons;

    private void Start()
    {
        _isActive = false;
    }

    private void Update()
    {

        if (_remainingTime > 0 && _isActive)
        {
            _remainingTime -= Time.deltaTime;
        }
        else if (_remainingTime < 0)
        {
            _remainingTime = 0;
            // stuff that happens if time runs out
        }
        int hours = Mathf.FloorToInt(_remainingTime / 3600);
        int minutes = Mathf.FloorToInt(_remainingTime / 60 % 60);
        int seconds = Mathf.FloorToInt(_remainingTime % 60);
        _timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    public void StartTimer()
    {
        _isActive = true;
    }
    public void ToggleTimer()
    {
        _isActive = !_isActive;
        foreach (var button in _timeControlButtons)
        {
            // if timer is active, hide buttons. if timer is inactive, show buttons
            button.SetActive(!_isActive);
        }
    }
    public void StopTimer()
    {
        _isActive = false;
    }

    public void AddHours(int pHours)
    {
        if (pHours > 0 || _remainingTime - pHours * -3600 > 0)
        {
            _remainingTime += pHours * 3600;
        }
    }
    public void AddMinutes(int pMinutes)
    {
        if (pMinutes > 0 || _remainingTime - pMinutes * -60 > 0)
        {
            _remainingTime += pMinutes * 60;
        }
    }
    public void AddSeconds(int pSeconds)
    {
        if (pSeconds > 0 || _remainingTime - pSeconds * -1 > 0)
        {
            _remainingTime += pSeconds;
        }
    }

}
