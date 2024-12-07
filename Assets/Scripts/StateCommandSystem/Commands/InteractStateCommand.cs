using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractStateCommand : NavigateStateCommand
{
    private Interactable _interactable;
    private Action<Interactable> _onInteract;
    
    public InteractStateCommand(Interactable interactable) : base(interactable.GetComponent<NavigationTarget>())
    {
        _interactable = interactable;
    }

    public InteractStateCommand(Interactable interactable, Action<Interactable> onInteract) : base(interactable.GetComponent<NavigationTarget>())
    {
        _interactable = interactable;
        _onInteract = onInteract;
    }
    
    public override void Invoke(StateCommandTarget stateCommandTarget)
    {
        var targetInteractor = GetRequiredStateCommandTargetComponent<Interactor>(stateCommandTarget);
        if (_onInteract != null)
        {
            targetInteractor.OnInteract += _onInteract;
        }
        targetInteractor.OnInteract += GetOnInteractAction(stateCommandTarget);
        targetInteractor.SetInteraction(_interactable);
        base.Invoke(stateCommandTarget);
    }

    public override void Cancel(StateCommandTarget stateCommandTarget)
    {
        var targetInteractor = GetRequiredStateCommandTargetComponent<Interactor>(stateCommandTarget);
        targetInteractor.OnInteract = null;
        targetInteractor.CancelInteraction();
        base.Cancel(stateCommandTarget);
    }
    
    private Action<Interactable> GetOnInteractAction(StateCommandTarget stateCommandTarget)
    {
        return _ =>
        {
            Done(stateCommandTarget);
        };
    }
}
