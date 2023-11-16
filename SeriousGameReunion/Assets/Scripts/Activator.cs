using System;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] KeyCode key;
    [SerializeField] bool active = false;
    [SerializeField] GameObject note;
    [SerializeField] private InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager.score = 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        active = true;
        if (other.gameObject.CompareTag("Note"))
        {
            note = other.gameObject;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        active = true;
        
        if (Input.GetKeyDown(key) && active)
        {
            Destroy(note); 
            inventoryManager.score += 1;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        active = false;
    }
}
