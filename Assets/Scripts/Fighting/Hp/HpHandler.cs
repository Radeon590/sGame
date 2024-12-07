using System;
using Fighting.Core;
using UnityEngine;

namespace Fighting.Hp
{
    public class HpHandler : MonoBehaviour, IHpHandler
    {
        [SerializeField] protected float hp;
        public float Hp => hp;
        public Action OnDead;
        public bool IsDead => hp <= 0;
        public bool HandleDamage(float damage)
        {
            hp -= damage;
            if (hp <= 0)
            {
                Destroy(gameObject);
                OnDead?.Invoke();
                return true;
            }

            return false;
        }
    }
}
