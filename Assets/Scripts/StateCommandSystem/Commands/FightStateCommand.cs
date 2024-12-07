using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStateCommand : NavigateStateCommand
{
    private FightTarget _fightTarget;
    private Dictionary<StateCommandTarget, Action<Fighter, IWeapon>> _onFighterDeadActions;
    private Dictionary<StateCommandTarget, Action<Fighter, IWeapon>> _onTargetDeadActions;
    
    public FightStateCommand(FightTarget fightTarget) : base(fightTarget.NavigationTarget)
    {
        _fightTarget = fightTarget;
        _onFighterDeadActions = new Dictionary<StateCommandTarget, Action<Fighter, IWeapon>>();
        _onTargetDeadActions = new Dictionary<StateCommandTarget, Action<Fighter, IWeapon>>();
    }

    public override void Invoke(StateCommandTarget stateCommandTarget)
    {
        Fighter fighter = GetRequiredStateCommandTargetComponent<Fighter>(stateCommandTarget);
        fighter.SetTarget(_fightTarget);
        Offset = fighter.Weapon.Range - 1;
        SubscribeFighter(stateCommandTarget);
        base.Invoke(stateCommandTarget);
    }

    public override void Cancel(StateCommandTarget stateCommandTarget)
    {
        base.Cancel(stateCommandTarget);
        Fighter fighter = GetRequiredStateCommandTargetComponent<Fighter>(stateCommandTarget);
        fighter.CancelFight();
        UnsibscribeFighter(stateCommandTarget);
    }

    private void SubscribeFighter(StateCommandTarget stateCommandTarget)
    {
        var onDeadAction = GetOnTargetDeadAction(stateCommandTarget);
        _fightTarget.OnDead += onDeadAction;
        _onTargetDeadActions.Add(stateCommandTarget, onDeadAction);
        // fighter death handling
        if (stateCommandTarget.TryGetComponent<FightTarget>(out var fighterFightTarget))
        {
            var onFighterDeadAction = GetOnFighterDeadAction(fighterFightTarget, stateCommandTarget);
            fighterFightTarget.OnDead += onFighterDeadAction;
            _onFighterDeadActions.Add(stateCommandTarget, onFighterDeadAction);
        }
    }

    private void UnsibscribeFighter(StateCommandTarget stateCommandTarget)
    {
        if (_onTargetDeadActions.ContainsKey(stateCommandTarget))
        {
            _fightTarget.OnDead -= _onTargetDeadActions[stateCommandTarget];
            _onTargetDeadActions.Remove(stateCommandTarget);
        }

        if (_onFighterDeadActions.ContainsKey(stateCommandTarget))
        {
            stateCommandTarget.GetComponent<FightTarget>().OnDead -= _onFighterDeadActions[stateCommandTarget];
            _onFighterDeadActions.Remove(stateCommandTarget);
        }
    }
    
    private Action<Fighter, IWeapon> GetOnTargetDeadAction(StateCommandTarget stateCommandTarget)
    {
        Action<Fighter, IWeapon> result = (_, _) =>
        {
            //Debug.Log($"OnTargetDead {stateCommandTarget.gameObject.name}");
            Done(stateCommandTarget);
        };
        result += (_, _) => _fightTarget.OnDead -= result;

        return result;
    }
    
    private Action<Fighter, IWeapon> GetOnFighterDeadAction(FightTarget fightTarget, StateCommandTarget stateCommandTarget)
    {
        Action<Fighter, IWeapon> result = (_, _) =>
        {
            //Debug.Log($"OnTargetDead {stateCommandTarget.gameObject.name}");
            UnsibscribeFighter(stateCommandTarget);
        };
        result += (_, _) => fightTarget.OnDead -= result;

        return result;
    }
}
