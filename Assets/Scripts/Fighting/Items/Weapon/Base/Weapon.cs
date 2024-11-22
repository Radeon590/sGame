using System;
using UnityEngine;

namespace Fighting.Items.Weapon.Base
{
    public abstract class Weapon : ScriptableObject, IWeapon
    {
        [SerializeField] protected float damage;
        [SerializeField] protected float rate;
        [SerializeField] protected float range;
        public float Range => range;
        public float Damage => damage;
        public virtual void UseEffect(Fighter fighter, FightTarget target)
        {
            target.Attack(this);
            OnEffect?.Invoke(fighter, target);
        }

        public Action<Fighter, FightTarget> OnEffect { get; set; }
    }
}