using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    private float _countdownTime = 10.0f;
    private float _timeStart;
    private float _timeEnd;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GetComponent<UIManager>();

        if(_uiManager == null)
        {
            Debug.LogError("Countdown - UI Manager not found");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //update current time display in UI
        TimeRemaining();
    }

    public void SetCountdown()
    {
        _timeStart = Time.time;
        _timeEnd = _timeStart + _countdownTime;
        _uiManager.DisplayCountdown();
    }

    void TimeRemaining()
    {
        if (_timeEnd - Time.time >= 0)
        {
            _uiManager.UpdateTimeRemainingText(_timeEnd - Time.time);
        }
        else
        {
            _uiManager.UpdateTimeRemainingText(0.00f);
        }
    }
}
