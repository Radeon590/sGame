using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public float Range { get; }
    public float Rate { get; }
    public float Damage { get; }
    public void UseEffect(Fighter fighter, FightTarget target);
    
    public Action<Fighter, FightTarget> OnEffect { get; set; }
}
