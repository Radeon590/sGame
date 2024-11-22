using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public Action<Interactor> OnInteract;

    public virtual void Interact(Interactor interactor)
    {
        OnInteract?.Invoke(interactor);
    }
}
