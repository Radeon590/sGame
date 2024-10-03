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
        interactor.Interact(this);
        OnInteract?.Invoke(interactor);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactor interactor))
        {
            Interact(interactor);
        }
    }
}
