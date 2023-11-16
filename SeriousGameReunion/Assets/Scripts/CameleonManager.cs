using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class CameleonManager : MonoBehaviour
{
    public static CameleonManager instance;
    
    [BoxGroup("Stat")] [SerializeField] private int goodStatHealth;
    [BoxGroup("Stat")] [SerializeField] private int goodStatEnergy;
    [BoxGroup("Stat")] [SerializeField] private int goodStatHappiness;
    [BoxGroup("Stat")] [SerializeField] private int goodStatHunger;
    [BoxGroup("Stat")] [SerializeField] private int badStatTiredness;
    [BoxGroup("Stat")] [SerializeField] private int badStatThickness;
    [BoxGroup("Stat")] [SerializeField] private int badStatSickness;
    
    [BoxGroup("Stat")] public int hungerPerHour = -5;

    [BoxGroup("Stat")] [SerializeField] private TextMeshProUGUI textHealth;
    [BoxGroup("Stat")] [SerializeField] private TextMeshProUGUI textEnergy;
    [BoxGroup("Stat")] [SerializeField] private TextMeshProUGUI textHapiness;
    [BoxGroup("Stat")] [SerializeField] private TextMeshProUGUI textHunger;

    [BoxGroup("Hidden Stats : Health")] public bool showHiddenStatHealth;
    [ShowIf("showHiddenStatHealth")] [BoxGroup("Hidden Stats : Health")] 
    [SerializeField] private int minHealthBfSickness = 40;
    [ShowIf("showHiddenStatHealth")] [BoxGroup("Hidden Stats : Health")] 
    [SerializeField] private int sicknessWhenUnhealthy = 1;
    [ShowIf("showHiddenStatHealth")] [BoxGroup("Hidden Stats : Health")] 
    [SerializeField] private int maxSicknessBfDeath = 20;
    
    [BoxGroup("Hidden Stats : Thickness")] public bool showHiddenStatHunger;
    [ShowIf("showHiddenStatHunger")] [BoxGroup("Hidden Stats : Thickness")] 
    [SerializeField] private int maxHunger = 100;
    [ShowIf("showHiddenStatHunger")] [BoxGroup("Hidden Stats : Thickness")] 
    [SerializeField] private int maxThickness = 100;
    
    [BoxGroup("Hidden Stats : Tiredness")] public bool showHiddenStatTiredness;
    [ShowIf("showHiddenStatTiredness")] [BoxGroup("Hidden Stats : Tiredness")] 
    [SerializeField] private int minEnergyBfTiredness = 30;
    [ShowIf("showHiddenStatTiredness")] [BoxGroup("Hidden Stats : Tiredness")] 
    [SerializeField] private int maxTiredness = 50;
    
    [BoxGroup("DEBUG")] public List<int> startingStats = new (7);
    


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
        InitStats();
    }
    
    
    void Update()
    {
        //debug seulement
        if (Input.GetKeyDown(KeyCode.V))
        {
            badStatSickness = maxSicknessBfDeath;
            DailyStatusEvolution();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChCamHunger(maxHunger*10);
            CheckHunger();
            
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            badStatTiredness += maxTiredness;
            ChCamEnergy(0);
        }
    }

    void InitStats()
    {
        goodStatHealth = startingStats[0];
        goodStatEnergy = startingStats[1];
        goodStatHappiness = startingStats[2];
        goodStatHunger = startingStats[3];
        badStatTiredness = startingStats[4];
        badStatThickness = startingStats[5];
        badStatSickness = startingStats[6];
        CheckStatus();
    }
    
    private void CheckStatus()
    {
        
        //mise à jour des stats visibles 
        textHealth.text = "Santé : " + goodStatHealth + "/100";
        textEnergy.text = "Energie : " + goodStatEnergy + "/100";;
        textHapiness.text = "Bonheur : " + goodStatHappiness + "/100";;
        textHunger.text = "Faim : " + goodStatHunger + "/" + maxHunger;
    }

    public void ChCamHealth(int value)
    {
        goodStatHealth += value;
        CheckStatus();
    }
    
    public void ChCamEnergy(int value)
    {
        goodStatEnergy += value;
        CheckTiredness();
        CheckStatus();
    }
    
    public void ChCamHappiness(int value)
    {
        goodStatHappiness += value;
        CheckStatus();
    }
    
    public void ChCamHunger(int value)
    {
        goodStatHunger += value;
        CheckHunger();
        CheckStatus();
    }


    public void DailyStatusEvolution() //se call à la fin de chaque jour dans le TimeManager.cs
    {
        CheckSickness();
    }

    public void CheckSickness()
    {
        if (goodStatHealth <= minHealthBfSickness)
        { //si Health <= stat avant d'être malade, gain dans la stat de maladie
            badStatSickness += sicknessWhenUnhealthy;
        }
        if (badStatSickness >= maxSicknessBfDeath)
        {
            UiManager.instance.t_raisonGameOver.text = "Raison : La maladie a mené votre caméléon à l'hôpital.";
            Defeat();
        }
    }

    public void CheckHunger()
    {
        if (goodStatHunger <= 0)
        {
            UiManager.instance.t_raisonGameOver.text = "Raison : La famine a mené votre caméléon à l'hôpital.";
            Defeat();
        }
        if (goodStatHunger >= maxHunger)
        {//si la faim est dépassé, la différence par 100 est rajouté en obésité
            badStatThickness += goodStatHunger - maxHunger;
        }
        if (badStatThickness >= maxThickness)
        {
            UiManager.instance.t_raisonGameOver.text = "Raison : L'obésité a mené votre caméléon à l'hôpital.";
            Defeat();
        }
    }

    public void CheckTiredness()
    {
        if (goodStatEnergy <= minEnergyBfTiredness)
        {//si Energy est en dessous de la limite, gain de fatigue (à chaque fois qu'on modifie Energy)
            badStatTiredness += minEnergyBfTiredness - goodStatEnergy;
        }
        if (badStatTiredness >= maxTiredness)
        {
            UiManager.instance.t_raisonGameOver.text = "Raison : La fatigue a emporté votre caméléon à l'hôpital.";
            Defeat();
        }
    }


    public void Defeat()
    {
        Time.timeScale = 0;
        UiManager.instance.menuInGame.SetActive(false);
        UiManager.instance.menuGameOver.SetActive(true);
    }
    
}

public enum CameleonStats
{
    Health,
    Energy,
    Happiness,
    Hunger,
}