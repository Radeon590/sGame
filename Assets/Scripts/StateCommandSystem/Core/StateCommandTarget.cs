using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCommandTarget : MonoBehaviour
{
    private StateCommand _currentStateCommand;
    public StateCommand CurrentStateCommand => _currentStateCommand;
    
    /// <summary>
    /// actual invoking from stateCommand onDone Action
    /// </summary>
    private Action<StateCommand, StateCommandTarget> _currentOnDone;

    /// <summary>
    /// OnDone preset
    /// </summary>
    public Action<StateCommand, StateCommandTarget> OnDone;

    public void SetStateCommand(StateCommand stateCommand)
    {
        if (_currentOnDone != null)
        {
            _currentStateCommand.OnDone -= _currentOnDone;
        }
        _currentStateCommand = stateCommand;
        _currentOnDone = OnDone;
        _currentStateCommand.OnDone += _currentOnDone;
    }

    public void InvokeStateCommand()
    {
        _currentStateCommand.Invoke(this);
    }

    public void CancelStateCommand()
    {
        _currentStateCommand.Cancel(this);
    }
    
    public void InvokeStateCommand(StateCommand stateCommand)
    {
        if (_currentStateCommand != null)
        {
            CancelStateCommand();
        }
        SetStateCommand(stateCommand);
        InvokeStateCommand();
    }

    private void OnDestroy()
    {
        if (_currentStateCommand != null 
            && _currentOnDone != null 
            && _currentStateCommand.OnDone != null)
        {
            _currentStateCommand.OnDone -= _currentOnDone;
        }
    }
}
