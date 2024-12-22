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

            if (_target != null && _target.gameObject != null)
            {
                _target.OnDead += OnTargetDead;
            }

            StartCoroutine(LifeTimeTimer());
        }

        private void Update()
        {
            if (_target == null) return;

            Vector2 direction = (_target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<FightTarget>(out FightTarget target) && target == _target)
            {
                OnHit?.Invoke(this);
            }
        }

        private IEnumerator LifeTimeTimer()
        {
            yield return new WaitForSeconds(lifetime);
            DestroyBullet();
        }

        private void OnTargetDead(Fighter fighter, IWeapon weapon)
        {
            DestroyBullet();
        }

        private void DestroyBullet()
        {
            if (_target != null && _target.gameObject != null)
            {
                try
                {
                    _target.OnDead -= OnTargetDead;
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"Failed to unsubscribe OnDead for {_target}: {ex.Message}");
                }
            }

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (_target != null && _target.gameObject != null)
            {
                try
                {
                    _target.OnDead -= OnTargetDead;
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"OnDestroy: Failed to unsubscribe from OnDead. Exception: {ex.Message}");
                }
            }
        }
    }
}
