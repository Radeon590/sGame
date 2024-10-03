using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDebug : MonoBehaviour
{
    [SerializeField] private Interactable interactable;

    private void Start()
    {
        interactable.OnInteract += Log;
    }

    public void Log(Interactor interactor)
    {
        Debug.Log($"TestDebug interactor {interactor.gameObject.name}");
    }
    
    public void Log()
    {
        Debug.Log("TestDebug log");
    }
}
