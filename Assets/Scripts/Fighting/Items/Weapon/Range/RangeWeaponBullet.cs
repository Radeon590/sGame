using System;
using UnityEngine;

namespace Fighting.Items.Weapon.Range
{
    public class RangeWeaponBullet : MonoBehaviour
    {
        [SerializeField] protected float speed = 3;
        public Action<RangeWeaponBullet> OnHit;
        private FightTarget _target;
        public FightTarget Target => _target;
        
        public void SetTarget(FightTarget target)
        {
            _target = target;
        }

        private void Update()
        {
            if (Target == null)
            {
                return;
            }
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, speed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<FightTarget>(out FightTarget target))
            {
                if (target == Target)
                {
                    OnHit?.Invoke(this);
                }
            }
        }
    }
}