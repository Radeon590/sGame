using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateStateCommand : StateCommand
{
    private Vector2 _targetPos;
    public float Offset = 0;
    
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
        Navigatable navigatable = GetRequiredStateCommandTargetComponent<Navigatable>(stateCommandTarget);
        navigatable.NavMeshAgent.stoppingDistance = Offset;
        navigatable.SetTarget(_targetPos);
        base.Invoke(stateCommandTarget);
    }

    public override void Cancel(StateCommandTarget stateCommandTarget)
    {
        Navigatable navigatable = GetRequiredStateCommandTargetComponent<Navigatable>(stateCommandTarget);
        navigatable.Stop();
        base.Cancel(stateCommandTarget);
    }
}
