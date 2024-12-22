using System;
using UnityEngine;

public abstract class StateCommand
{
    public Action<StateCommand, StateCommandTarget> OnDone;

    public virtual void Invoke(StateCommandTarget stateCommandTarget)
    {
    }

    public virtual void Cancel(StateCommandTarget stateCommandTarget)
    {
    }

    protected virtual void Done(StateCommandTarget stateCommandTarget)
    {
        Cancel(stateCommandTarget);
        OnDone?.Invoke(this, stateCommandTarget);
    }

    protected T GetRequiredStateCommandTargetComponent<T>(StateCommandTarget stateCommandTarget)
    {
        try
        {
            if (stateCommandTarget == null)
            {
                throw new ArgumentNullException(nameof(stateCommandTarget), "StateCommandTarget is null.");
            }

            if (stateCommandTarget.TryGetComponent(out T navigatable))
            {
                return navigatable;
            }
            else
            {
                throw new Exception($"Cannot get {typeof(T).Name} from CommandTarget object when invoking {GetType().Name}.");
            }
        }
        catch (ArgumentNullException ex)
        {
            Debug.LogWarning($"ArgumentNullException in GetRequiredStateCommandTargetComponent: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"Exception in GetRequiredStateCommandTargetComponent: {ex.Message}");
            throw;
        }
    }

}
