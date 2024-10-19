using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateCommand
{
    public virtual void Invoke(StateCommandTarget stateCommandTarget)
    {
        
    }

    public virtual void Cancel(StateCommandTarget stateCommandTarget)
    {
        
    }

    protected T GetRequiredStateCommandTargetComponent<T>(StateCommandTarget stateCommandTarget)
    {
        if (stateCommandTarget.TryGetComponent(out T navigatable))
        {
            return navigatable;
        }
        else
        {
            throw new Exception($"cant get {typeof(T)} from CommandTarget object when invoke {GetType()}");
        }
    }
}
