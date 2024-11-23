using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStateCommand : NavigateStateCommand
{
    private FightTarget _fightTarget;
    
    public FightStateCommand(FightTarget fightTarget) : base(fightTarget.NavigationTarget)
    {
        _fightTarget = fightTarget;
    }

    public override void Invoke(StateCommandTarget stateCommandTarget)
    {
        Fighter fighter = GetRequiredStateCommandTargetComponent<Fighter>(stateCommandTarget);
        fighter.SetTarget(_fightTarget);
        Offset = fighter.Weapon.Range - 1;
        base.Invoke(stateCommandTarget);
    }

    public override void Cancel(StateCommandTarget stateCommandTarget)
    {
        base.Cancel(stateCommandTarget);
        Fighter fighter = GetRequiredStateCommandTargetComponent<Fighter>(stateCommandTarget);
        fighter.CancelFight();
    }
}
