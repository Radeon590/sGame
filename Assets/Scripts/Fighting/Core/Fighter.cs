using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    private IWeapon _weapon;

    public IWeapon Weapon
    {
        set
        {
            if (_weapon != null)
            {
                _weapon.OnEffect -= OnWeaponEffect;
            }

            _weapon = value;
            _weapon.OnEffect += OnWeaponEffect;
        }
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

    public void OnWeaponEffect(Fighter fighter, FightTarget target)
    {
        if (fighter != this)
        {
            Debug.Log("trying to invoke onWeaponEffect from weapon in hands of another fighter");
            return;
        }
        
        target.Attack(_weapon);
    }
}
