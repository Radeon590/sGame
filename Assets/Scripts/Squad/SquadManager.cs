using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SquadManager : MonoBehaviour
{
    public List<SquadUnit> SquadUnits;
    private StateCommand _currentStateCommand;

    public void SetCommand(StateCommand stateCommand)
    {
        _currentStateCommand = stateCommand;
    }

    public void InvokeCurrentCommand()
    {
        foreach (var commandTarget in SquadUnits)
        {
            commandTarget.StateCommandTarget.InvokeStateCommand();
        }
    }

    public void CancelCurrentCommand()
    {
        foreach (var commandTarget in SquadUnits)
        {
            commandTarget.StateCommandTarget.CancelStateCommand();
        }
    }
    
    public void InvokeCommand(StateCommand stateCommand)
    {
        SetCommand(stateCommand);
        foreach (var commandTarget in SquadUnits)
        {
            commandTarget.StateCommandTarget.InvokeStateCommand(stateCommand);
        }
    }
}
