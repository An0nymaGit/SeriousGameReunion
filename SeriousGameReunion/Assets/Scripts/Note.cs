using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Note : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        speed = 100f;
    }

    void Start()
    {
        rb.velocity = new Vector3(speed, 0);
    }
}
