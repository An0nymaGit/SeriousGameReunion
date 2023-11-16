using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sleep : MonoBehaviour
{
    [SerializeField] [ReadOnly] private int durationInHours = 0;
    [SerializeField] [ReadOnly] private int durationInMinutes = 0;
    
    [SerializeField] private Button b_Sleep;
    [SerializeField] private TextMeshProUGUI t_TimeToSleep;
    
    [SerializeField] private Button b_AddTimeToSleep;
    [SerializeField] private Button b_ReduceTimeToSleep;
    [SerializeField] private Button b_AddTimeToSleepHours;
    [SerializeField] private Button b_ReduceTimeToSleepHours;
    [SerializeField] private int durationInMinutesToChange = 10;

    

    [SerializeField] private int maxHoursToSleep;
    [SerializeField] private int minHoursToSleep;
    [SerializeField] private int minMinutesToSleep;


    private void Start()
    {
        durationInHours = 8;
        durationInMinutes = 0;
        TimeUpdate();
    }

    public void GoToSleep()
    {
        TimeManager.instance.AddTime(durationInHours, durationInMinutes);
    }

    public void AddTimeInMinutes()
    {
        durationInMinutes += durationInMinutesToChange;
        CheckTime();
    }
    
    public void ReduceTimeInMinutes()
    {
        durationInMinutes -= durationInMinutesToChange;
        CheckTime();
    }
    
    public void AddTimeInHours()
    {
        durationInHours += 1;
        CheckTime();
    }
    
    public void ReduceTimeInHours()
    {
        durationInHours -= 1;
        CheckTime();
    }

    void CheckTime()
    {
        if (durationInMinutes == 60)
        {
            durationInHours += 1;
            durationInMinutes = 0;
        }
        else if (durationInMinutes < 0)
        {
            durationInHours -= 1;
            durationInMinutes = 50;
        }
        
        if (durationInHours == maxHoursToSleep)
        {
            b_AddTimeToSleep.interactable = false;
            b_AddTimeToSleepHours.interactable = false;
        }
        else if (durationInHours == minHoursToSleep && durationInMinutes == minMinutesToSleep)
        {
            b_ReduceTimeToSleep.interactable = false;
            b_ReduceTimeToSleepHours.interactable = false;

        }
        else
        {
            b_AddTimeToSleep.interactable = true;
            b_ReduceTimeToSleep.interactable = true;
            b_AddTimeToSleepHours.interactable = true;
            b_ReduceTimeToSleepHours.interactable = true;
        }
        TimeUpdate();
    }
    
    void TimeUpdate()
    {
        
        if (durationInMinutes == 0)
        {
            t_TimeToSleep.text = durationInHours + " : 0" + durationInMinutes;
        }
        else
        {
            t_TimeToSleep.text = durationInHours + " : " + durationInMinutes;
        }
    }
}
