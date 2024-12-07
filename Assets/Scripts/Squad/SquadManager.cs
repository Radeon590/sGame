using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SquadManager : MonoBehaviour
{
    private List<SquadUnit> SquadUnits;
    private StateCommand _currentStateCommand;
    private Action<StateCommand, StateCommandTarget> _currentOnDone;

    public void SetSquadUnits(List<SquadUnit> squadUnits)
    {
        if (SquadUnits != null)
        {
            foreach (var squadUnit in SquadUnits)
            {
                if (_currentOnDone != null)
                {
                    squadUnit.OnDone -= _currentOnDone;
                }
            }
        }

        _currentOnDone = GetOnDoneAction();
        SquadUnits = squadUnits;
        foreach (var squadUnit in squadUnits)
        {
            squadUnit.SquadManager = this;
            squadUnit.OnDone += _currentOnDone;
        }
    }

    public void RemoveSquadUnit(SquadUnit squadUnit)
    {
        squadUnit.SquadManager = null;
        squadUnit.OnDone -= _currentOnDone;
        SquadUnits.Remove(squadUnit);
    }
    
    public void SetCommand(StateCommand stateCommand)
    {
        foreach (var squadUnit in SquadUnits)
        {
            squadUnit.SetStateCommand(stateCommand);
        }
    }

    public void InvokeCurrentCommand()
    {
        foreach (var squadUnit in SquadUnits)
        {
            squadUnit.InvokeStateCommand();
        }
    }

    public void CancelCurrentCommand()
    {
        foreach (var squadUnit in SquadUnits)
        {
            squadUnit.CancelStateCommand();
        }
    }
    
    public void InvokeCommand(StateCommand stateCommand)
    {
        foreach (var squadUnit in SquadUnits)
        {
            squadUnit.InvokeStateCommand(stateCommand);
        }
    }

    private Action<StateCommand, StateCommandTarget> GetOnDoneAction()
    {
        return (command, invoker) =>
        {
            var invokerSquadUnit = invoker as SquadUnit;
            if (invokerSquadUnit == null)
            {
                throw new Exception("invoker is not squad unit. cant cancel command in another squad units");
            }
            
            foreach (var squadUnit in SquadUnits)
            {
                if (squadUnit != invokerSquadUnit)
                {
                    squadUnit.CancelStateCommand();
                }
            }
        };
    }
}
