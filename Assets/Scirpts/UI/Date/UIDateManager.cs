using System;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDateManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI TimerDateTest;
    [SerializeField] private TextMeshProUGUI TimerTimeTest;
    [SerializeField] private float TurnoverTime = 1f;
    private float Timer;

    void Start()
    {
        Timer = TurnoverTime;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimerTest();
    }

    #region UpdateTimer
    void UpdateTimerTest()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            Timer = TurnoverTime;
            if (TimerDateTest != null || TimerTimeTest != null)
            {
                DateTime now = DateTime.Now;
                //string weekDay = now.DayOfWeek switch
                //{
                //    DayOfWeek.Monday => "周一",
                //    DayOfWeek.Tuesday => "周二",
                //    DayOfWeek.Wednesday => "周三",
                //    DayOfWeek.Thursday => "周四",
                //    DayOfWeek.Friday => "周五",
                //    DayOfWeek.Saturday => "周六",
                //    DayOfWeek.Sunday => "周日",
                //    _ => ""
                //};
                TimerDateTest.text = $"{now.Year}/{now.Month:d2}/{now.Day:d2},{now.DayOfWeek}";
                TimerTimeTest.text = $"{now.Hour:d2}:{now.Minute:d2}";
            }
        }
    }
    #endregion

}
