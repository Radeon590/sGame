using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateStateCommand : StateCommand
{
    private NavigationTarget _target;
    protected Vector2 _targetPos;
    public float Offset = 0;
    
    protected Navigatable _navigatable;
    
    public NavigateStateCommand(NavigationTarget navigationTarget)
    {
        _target = navigationTarget;
    }
    
    public NavigateStateCommand(Vector3 navigationTarget)
    {
        _targetPos = navigationTarget;
    }

    public override void Invoke(StateCommandTarget stateCommandTarget)
    {
        _navigatable = GetRequiredStateCommandTargetComponent<Navigatable>(stateCommandTarget);
        _navigatable.NavMeshAgent.stoppingDistance = Offset;
        if (_target != null)
        {
            _navigatable.SetTarget(_target);
        }
        else
        {
            _navigatable.SetTarget(_targetPos);
        }
        base.Invoke(stateCommandTarget);
    }

    public override void Cancel(StateCommandTarget stateCommandTarget)
    {
        _navigatable = GetRequiredStateCommandTargetComponent<Navigatable>(stateCommandTarget);
        _navigatable.Stop();
        base.Cancel(stateCommandTarget);
    }
}
