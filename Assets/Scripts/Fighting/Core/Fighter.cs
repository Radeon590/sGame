using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    private IWeapon _weapon;

    public IWeapon Weapon
    {
        set => _weapon = value;
        get => _weapon;
    }
    public FightTarget Target;

    public void SetTarget(FightTarget target)
    {
        Target = target;
    }

    public void CancelFight()
    {
        Target = null;
    }

    private void FixedUpdate()
    {
        if (Target != null)
        {
            if (Vector2.Distance(transform.position, Target.transform.position) <= Weapon.Range)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        Weapon.UseEffect(this, Target);
    }
}
