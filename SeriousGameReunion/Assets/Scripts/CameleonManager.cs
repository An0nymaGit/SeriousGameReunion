using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class CameleonManager : MonoBehaviour
{
    [BoxGroup("Stat")] [SerializeField] private int goodStatHealth;
    [BoxGroup("Stat")] [SerializeField] private int goodStatEnergy;
    [BoxGroup("Stat")] [SerializeField] private int goodStatHappiness;
    [BoxGroup("Stat")] [SerializeField] private int goodStatHunger;
    
    [BoxGroup("Stat")] public int badStatTiredness;
    [BoxGroup("Stat")] public int badStatThickness;
    [BoxGroup("Stat")] public int badStatSickness;

    [BoxGroup("Stat")] [SerializeField] private TextMeshProUGUI textHealth;
    [BoxGroup("Stat")] [SerializeField] private TextMeshProUGUI textEnergy;
    [BoxGroup("Stat")] [SerializeField] private TextMeshProUGUI textHapiness;
    [BoxGroup("Stat")] [SerializeField] private TextMeshProUGUI textHunger;
    
    [BoxGroup("DEBUG")] public List<int> startingStats = new (7);
    
    
    
    
    void Start()
    {
        InitStats();
    }
    
    
    void Update()
    {
        
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
        if (goodStatHealth >= 40)
        {
            
        }

        textHealth.text = "Santé : " + goodStatHealth + "/100";
        textEnergy.text = "Energie : " + goodStatEnergy + "/100";;
        textHapiness.text = "Bonheur : " + goodStatHappiness + "/100";;
        textHunger.text = "Faim : " + goodStatHunger + "/100";;

    }

    public void ChCamHealth(int value)
    {
        goodStatHealth += value;
        CheckStatus();
    }
    
    public void ChCamEnergy(int value)
    {
        goodStatEnergy += value;
        CheckStatus();
    }
    
    public void ChCamHappiness(int value)
    {
        goodStatHappiness += value;
        CheckStatus();
    }
    
    public void ChCamnHunger(int value)
    {
        goodStatHunger += value;
        CheckStatus();
    }
    
}

public enum CameleonStats
{
    Health,
    Energy,
    Happiness,
    Hunger,
}