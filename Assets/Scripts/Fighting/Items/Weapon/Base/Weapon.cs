﻿using System;
using Inventory;
using Inventory.Gameplay;
using UnityEngine;

namespace Fighting.Items.Weapon.Base
{
    public abstract class Weapon : Item, IWeapon
    {
        [SerializeField] protected float damage;
        [SerializeField] protected float rate;
        [SerializeField] protected float range;
        public float Damage => damage;
        public float Rate => rate;
        public float Range => range;
        
        public virtual void UseEffect(Fighter fighter, FightTarget target)
        {
            target.Attack(fighter, this);
            OnEffect?.Invoke(fighter, target);
        }

        public Action<Fighter, FightTarget> OnEffect { get; set; }
    }
}