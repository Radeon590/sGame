using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateStateCommand : StateCommand
{
    private Vector2 _targetPos;
    private Navigatable _navigatable;
    
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
        if (StateCommandTarget.TryGetComponent(out Navigatable navigatable))
        {
            _navigatable = navigatable;
        }
        else
        {
            throw new Exception("cant get Navigatable from CommandTarget object when execute NavigateToCommand");
        }
        _navigatable.SetTarget(_targetPos);
    }

    public override void Cancel()
    {
        _navigatable.Stop();
    }
}
