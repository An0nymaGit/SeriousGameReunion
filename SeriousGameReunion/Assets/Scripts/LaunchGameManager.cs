using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class LaunchGameManager : MonoBehaviour
{
    [BoxGroup("Menus")] [SerializeField] private GameObject menuInGame;
    [BoxGroup("Menus")] [SerializeField] private List<GameObject> menusActivity;
    [BoxGroup("Menus")] [SerializeField] private List<Button> buttonsActivity;
    
    
    
    public void ReturnToMenuInGame()
    {
        //désactive tous les menus 
        foreach (var menu in menusActivity)
        {
            menu.SetActive(false);
        }
        //réactive tous les boutons
        foreach (var button in buttonsActivity)
        {
            button.interactable = true;
        }
    }

    public void DoActivity(int index)
    {
        //active le menu d'UI associé
        menusActivity[index].SetActive(true);
        Debug.Log(menusActivity[index].name);
        
        //désactive les boutons des autres menu
        foreach (var button in buttonsActivity)
        {
            button.interactable = false;
        }
        
    }
    
    
}


