using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateCommand
{
    protected StateCommandTarget StateCommandTarget;

    public virtual void Invoke(StateCommandTarget stateCommandTarget)
    {
        StateCommandTarget = stateCommandTarget;
    }

    public virtual void Cancel()
    {
        
    }
}
