using System;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform chameleon;

    [SerializeField] private float jumpHeight = 3;
    [SerializeField] private float jumpPower = 5;
    [SerializeField] private float jumpDuration = 2;
    
    private void Start()
    {
        chameleon.DOJump(chameleon.position + Vector3.up * jumpHeight, jumpPower, 1, jumpDuration);
    }
}