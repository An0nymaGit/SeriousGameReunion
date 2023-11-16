using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Application = UnityEngine.Application;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    
    public GameObject menuInGame;
    public GameObject menuGameOver;

    public TextMeshProUGUI t_raisonGameOver;
    
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
        Time.timeScale = 0;
    }

    
    void Update()
    {
        //Debug.Log(Time.timeScale);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        //Debug.Log("Pause");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        //Debug.Log("Resume");
    }

    public void QuitGame()
    {
        Application.Quit();
        //Debug.Log("Quit");
    }
    
}
