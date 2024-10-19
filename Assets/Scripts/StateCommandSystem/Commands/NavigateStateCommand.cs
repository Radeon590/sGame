using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateStateCommand : StateCommand
{
    private Vector2 _targetPos;
    
    public NavigateStateCommand(NavigationTarget navigationTarget) 
        : this(navigationTarget.transform.position)
    {
        
    }
    
    public NavigateStateCommand(Vector3 navigationTarget)
    {
        _targetPos = navigationTarget;
    }

    public override void Invoke(StateCommandTarget stateCommandTarget)
    {
        base.Invoke(stateCommandTarget);
        Navigatable navigatable = GetRequiredStateCommandTargetComponent<Navigatable>(stateCommandTarget);
        navigatable.SetTarget(_targetPos);
    }

    public override void Cancel(StateCommandTarget stateCommandTarget)
    {
        Navigatable navigatable = GetRequiredStateCommandTargetComponent<Navigatable>(stateCommandTarget);
        navigatable.Stop();
    }
}
