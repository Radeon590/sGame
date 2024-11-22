using Fighting.Core;
using UnityEngine;

namespace Fighting.Hp
{
    public class HpHandler : MonoBehaviour, IHpHandler
    {
        [SerializeField] protected float hp;
        public float Hp => hp;
        public void HandleDamage(float damage)
        {
            hp -= damage;
            if (hp <= 0)
            {
                Debug.Log($"{name} destroyed");
            }
        }
    }
}
