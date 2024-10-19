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
            commandTarget.InvokeStateCommand();
        }
    }

    public void CancelCurrentCommand()
    {
        foreach (var commandTarget in ControlledCommandTargets)
        {
            commandTarget.CancelStateCommand();
        }
    }
    
    public void InvokeCommand(StateCommand stateCommand)
    {
        foreach (var commandTarget in ControlledCommandTargets)
        {
            commandTarget.InvokeStateCommand(stateCommand);
        }
    }
}
