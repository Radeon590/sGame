using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCommandTarget : MonoBehaviour
{
    private StateCommand _currentStateCommand;
    
    public void SetStateCommand(StateCommand stateCommand)
    {
        _currentStateCommand = stateCommand;
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
}
