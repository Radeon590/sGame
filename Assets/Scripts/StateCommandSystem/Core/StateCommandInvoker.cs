using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCommandInvoker : MonoBehaviour
{
    public List<StateCommandTarget> ControlledCommandTargets;
    private StateCommand _currentStateCommand;

    public void SetCommand(StateCommand stateCommand)
    {
        _currentStateCommand = stateCommand;
    }

    public void InvokeCurrentCommand()
    {
        foreach (var commandTarget in ControlledCommandTargets)
        {
            _currentStateCommand.Invoke(commandTarget);
        }
    }

    public void CancelCurrentCommand()
    {
        _currentStateCommand.Cancel();
    }
    
    public void InvokeCommand(StateCommand stateCommand)
    {
        if (_currentStateCommand != null)
        {
            CancelCurrentCommand();
        }
        SetCommand(stateCommand);
        InvokeCurrentCommand();
    }
}
