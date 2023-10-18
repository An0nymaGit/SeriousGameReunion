using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class LaunchGameManager : MonoBehaviour
{
    [BoxGroup("Menus")] [SerializeField] private GameObject menuInGame;
    [BoxGroup("Menus")] [SerializeField] private List<GameObject> menusActivity;
    
    public void ReturnToMenuInGame()
    {
        foreach (var menu in menusActivity)
        {
            menu.SetActive(false);
        }
    }

    public void DoActivity(int index)
    {
        menusActivity[index].SetActive(true);
        Debug.Log(menusActivity[index].name);
    }
}


