using System;
using System.Collections;
using Fighting.Hp;
using UnityEngine;

namespace Fighting.Items.Weapon.Range
{
    public class RangeWeaponBullet : MonoBehaviour
    {
        [SerializeField] protected float speed = 3;
        [SerializeField] protected float lifetime = 5;
        public Action<RangeWeaponBullet> OnHit;
        private Fighter _fighter;
        public Fighter Fighter => _fighter;
        private FightTarget _target;
        public FightTarget Target => _target;
        
        public void SetTarget(Fighter fighter, FightTarget target)
        {
            _fighter = fighter;
            _target = target;
            _target.OnDead += OnTargetDead;
            StartCoroutine(LifeTimeTimer());
        }

        private void Update()
        {
            if (Target == null)
            {
                return;
            }

            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<FightTarget>(out FightTarget target))
            {
                if (target == Target)
                {
                    OnHit?.Invoke(this);
                }
            }
        }

        private IEnumerator LifeTimeTimer()
        {
            yield return new WaitForSeconds(lifetime);
            Destroy(gameObject);
        }

        private void OnTargetDead(Fighter fighter, IWeapon weapon)
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            try
            {
                _target.OnDead -= OnTargetDead;
            }
            catch (InvalidOperationException)
            {
                // значит, таргет уже мёртв
            }
        }
    }
}