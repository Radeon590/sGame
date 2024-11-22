using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMousePosition : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public Vector3 mouseWorldPosition;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
    }
}

