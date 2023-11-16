using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateNote : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float _timer;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private GameObject spawn;
    
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > timeToSpawn)
        {
            _timer = 0;
            Instantiate(prefab, spawn.transform.position, Quaternion.identity);
        }
    }
}
