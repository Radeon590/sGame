using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    public Action<Interactable> OnInteract;
    
    public void Interact(Interactable interactable)
    {
        OnInteract?.Invoke(interactable);
    }
}
