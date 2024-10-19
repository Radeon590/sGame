using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractStateCommand : NavigateStateCommand
{
    private Interactable _interactable;
    private Action<Interactable> _onInteract;

    public InteractStateCommand(Interactable interactable, Action<Interactable> onInteract) : base(interactable.GetComponent<NavigationTarget>())
    {
        _interactable = interactable;
        _onInteract = onInteract;
    }
    
    public override void Invoke(StateCommandTarget stateCommandTarget)
    {
        var targetInteractor = GetRequiredStateCommandTargetComponent<Interactor>(stateCommandTarget);
        targetInteractor.OnInteract += _onInteract;
        targetInteractor.SetInteraction(_interactable);
        base.Invoke(stateCommandTarget);
    }

    public override void Cancel(StateCommandTarget stateCommandTarget)
    {
        var targetInteractor = GetRequiredStateCommandTargetComponent<Interactor>(stateCommandTarget);
        targetInteractor.OnInteract -= _onInteract;
        targetInteractor.CancelInteraction();
        base.Cancel(stateCommandTarget);
    }
}
