using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Timeline;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;
    [Header("Timer Setting")]
    public float currentTime;
 
    [Header("Limit Settings")]
     public float timerLimit;

    [Header("Format Settings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();
    private LevelPointManager levelPointManager;
    private void Start()
    {

        levelPointManager=GameObject.FindObjectOfType<LevelPointManager>();
        currentTime = levelPointManager.levelTime;
        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundredDecimal, "0.00");
        timerLimit = 0;
    }
    void Update()
    {
         currentTime -= Time.deltaTime  ;
        if( currentTime <= timerLimit)  
        {
             setTimerText();
            timerText.color = Color.red;
            enabled = false;
        }
        setTimerText();
    }
    private void setTimerText()
    {
        timerText.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();
    }
    public enum TimerFormats
    { 
    Whole ,
    TenthDecimal,
    HundredDecimal
    }
}
