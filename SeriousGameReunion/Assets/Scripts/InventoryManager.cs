using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Transactions;
using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public int score;
    [SerializeField] public List<Item> inventory;
    [SerializeField] public KeyCode key1;
    [SerializeField] public KeyCode key2;

    void Start()
    {
        inventory.Add(new HealthyDish() { amount = 0, name = "Gratin de chouchou"});
        inventory.Add(new UnhealthyDish() { amount = 0, name = "Pain bouchon gratiné"});
    }
    void Update()
    {
        if (score == 3)
        {
            var random = Random.Range(0, 2);
            inventory[random].amount += 1;
            Debug.Log(inventory);
            score = 0;
        }
        
        CheatCode();
    }
    
    void CheatCode()
     {
         if (Input.GetKeyDown(key1))
         {
             inventory[0].amount += 1;
         }

         if (Input.GetKeyDown(key2))
         {
             inventory[1].amount += 1;
         }
     }
}


[Serializable] public class Item
{
    public int amount;
    public string name;
    public int hungerIncrease = 10;
    public int healthIncrease = 10;
}

[Serializable] public class HealthyDish : Item 
{
}

[Serializable] public class UnhealthyDish : Item 
{
    
}