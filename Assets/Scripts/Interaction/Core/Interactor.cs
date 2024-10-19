using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    private bool _isActive = false;
    private Interactable _targetInteractable;
    
    public Action<Interactable> OnInteract;

    public void SetInteraction(Interactable interactable)
    {
        _targetInteractable = interactable;
        _isActive = true;
    }

    public void CancelInteraction()
    {
        _isActive = false;
        _targetInteractable = null;
    }
    
    public void Interact(Interactable interactable)
    {
        if (_isActive && interactable == _targetInteractable)
        {
            OnInteract?.Invoke(interactable);
            interactable.Interact(this);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            Interact(interactable);
        }
    }
}
