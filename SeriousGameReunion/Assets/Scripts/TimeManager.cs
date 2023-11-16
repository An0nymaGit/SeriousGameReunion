using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    
    [BoxGroup("Timer")] [SerializeField] private TextMeshProUGUI textTime;
    [BoxGroup("Timer")] [SerializeField] private TextMeshProUGUI textDay;
    [BoxGroup("Timer")] public int tempsMinute;
    [BoxGroup("Timer")] public int tempsHeure;
    [BoxGroup("Timer")] public int tempsJour;
    [BoxGroup("Timer")] [SerializeField] public Image imageHeure;
    [BoxGroup("Timer")] public Color[] colorsJourNuit;
    [BoxGroup("Timer")] public int[] horaireJourNuit;
    [BoxGroup("Timer")] [SerializeField] private int delayTime = 10;
    [BoxGroup("Timer")] [SerializeField] private int valueTime = 1;

    [BoxGroup("DEBUG")] [SerializeField] private KeyCode keyAdd;
    [BoxGroup("DEBUG")] [SerializeField] private int minutesAdd = 1;
    [BoxGroup("DEBUG")] [SerializeField] private int heuresAdd = 0;
    [BoxGroup("DEBUG")] [SerializeField] private int initMinute = 0;
    [BoxGroup("DEBUG")] [SerializeField] private int initHeure = 8;
    [BoxGroup("DEBUG")] [SerializeField] private int initJour = 1;
        

    
    private void Awake()
    {
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        }
    }
    
    void Start()
    {
        tempsMinute = initMinute;
        tempsHeure = initHeure;
        tempsJour = initJour;
        TimeUpdate();
        
    }

    void FixedUpdate()
    {
        if (Input.GetKey(keyAdd))
        {
            Debug.Log("Add");
            AddTime(heuresAdd,minutesAdd);
        }
    }

    public void LaunchTimer()
    {
        StartCoroutine(AddingTime());
    }
    
    public IEnumerator AddingTime()
    {
        yield return new WaitForSecondsRealtime(delayTime);
        AddTime(0,valueTime);
        //Debug.Log("+1 min");
        StartCoroutine(AddingTime());
    }
    
    
    void TimeUpdate()
    {
        if (tempsMinute <= 9)
        {
            textTime.text = tempsHeure + "h0" + tempsMinute;
        }
        else
        {
            textTime.text = tempsHeure + "h" + tempsMinute;
        }
        textDay.text = "Jour " + tempsJour;
        
        if (tempsHeure >= horaireJourNuit[0] || tempsHeure < horaireJourNuit[1])
        {
            //Debug.Log("soir");
            imageHeure.color = colorsJourNuit[1];
        }
        else
        {
            //Debug.Log("jour");
            imageHeure.color = colorsJourNuit[0];
        }
        
    }

    public void AddTime(int heures, int minutes)
    {
        for (int i = 0; i < minutes; i++)
        {
            tempsMinute += 1;
            if (tempsMinute >= 60)
            {
                tempsHeure += 1;
                tempsMinute = 0;
                CameleonManager.instance.ChCamHunger(CameleonManager.instance.hungerPerHour);
                if (tempsHeure >= 24)
                {
                    tempsJour += 1;
                    tempsHeure = 0;
                    CameleonManager.instance.DailyStatusEvolution();
                }
            }
        }

        for (int i = 0; i < heures; i++)
        {
            tempsHeure += 1;
            if (tempsHeure >= 24)
            {
                tempsJour += 1;
                tempsHeure = 0;
            }
        }
        
        TimeUpdate();
    }
    
}
