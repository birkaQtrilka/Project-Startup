using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] bool _isActive;
    [SerializeField] float _remainingTime;
    [SerializeField] int _hours;
    [SerializeField] int _minutes;

    [Header("References")]
    [SerializeField] TextMeshProUGUI _hoursText;
    [SerializeField] TextMeshProUGUI _minutesText;
    [SerializeField] TextMeshProUGUI _totalTimeText;
    [SerializeField] TMP_InputField _hourInput;
    [SerializeField] TMP_InputField _minuteInput;
    [SerializeField] GameObject _setTimeButton;
    [SerializeField] GameObject _startButton;
    [SerializeField] GameObject _pauseButton;
    [SerializeField] GameObject _stopButton;

    

    private void Start()
    {
        _isActive = false;
        _totalTimeText.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(false);
        _stopButton.gameObject.SetActive(false);
        _hourInput.gameObject.SetActive(false);
        _minuteInput.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateTime();
    }

    void UpdateTime()
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
        _totalTimeText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    public void SetHours(string pHours)
    {
        if (string.IsNullOrEmpty(pHours))
        {
            _hours = 0;
        }
        else
        {
            _hours = int.Parse(pHours);
        }

        _remainingTime = _hours * 3600 + _minutes * 60;
        UpdateTimeText();
    }
    public void SetMinutes(string pMinutes)
    {

        if (string.IsNullOrEmpty(pMinutes))
        {
            _minutes = 0;
        }
        else
        {
            _minutes = int.Parse(pMinutes);
        }

        _remainingTime = _hours * 3600 + _minutes * 60;
        UpdateTimeText();
    }
    void UpdateTimeText()
    {
        string twoDigitHelperMin = _minutes%60 >= 10 ? "" : "0";
        string twoDigitHelperHr = _hours >= 10 ? "" : "0";

        int hours = Mathf.FloorToInt(_remainingTime / 3600);
        int minutes = Mathf.FloorToInt(_remainingTime / 60 % 60);

        _minutesText.text = twoDigitHelperMin + minutes.ToString();
        _hoursText.text = twoDigitHelperHr + hours.ToString();

    }

    public void StartTime()
    {
        _remainingTime = _hours * 3600 + _minutes * 60;
        _isActive = true;

        // hide input fields

        _hoursText.gameObject.SetActive(false);
        _minutesText.gameObject.SetActive(false);
        _hourInput.gameObject.SetActive(false);
        _minuteInput.gameObject.SetActive(false);
        _totalTimeText.gameObject.SetActive(true);

        _setTimeButton.gameObject.SetActive(false);
        _startButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
        _stopButton.gameObject.SetActive(true);
    }

    public void PauseTime()
    {
        _isActive = !_isActive;
    }

    public void StopTime()
    {
        _isActive = false;
        _remainingTime = _hours * 3600 + _minutes * 60;

        // show input fields

        _hoursText.gameObject.SetActive(true);
        _minutesText.gameObject.SetActive(true);
        _totalTimeText.gameObject.SetActive(false);

        _setTimeButton.gameObject.SetActive(true);
        _startButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
        _stopButton.gameObject.SetActive(false);
    }

    public void SetTime()
    {
        _setTimeButton.gameObject.SetActive(false);
        _hourInput.gameObject.SetActive(true);
        _minuteInput.gameObject.SetActive(true);
    }





    

}
