using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D playerHitbox;
    private CinemachineConfiner2D _cameraConfiner;


    private void Awake()
    {
        _cameraConfiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }

    private void Start()
    {
        StartCoroutine(CameraChange());
    }

    IEnumerator CameraChange()
    {
        foreach (PolygonCollider2D collision in FindObjectsOfType<PolygonCollider2D>())
        {
            if (!playerHitbox.IsTouching(collision)) continue;
            _cameraConfiner.m_BoundingShape2D = collision.GetComponent<PolygonCollider2D>();
        }

        yield return new WaitForFixedUpdate();
        StartCoroutine(CameraChange());
    }
}