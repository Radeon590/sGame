using UnityEngine;

namespace Fighting.Items.Weapon.Range
{
    [CreateAssetMenu(fileName = "RangeWeapon", menuName = "FightingItems/Weapon/RangeWeapon", order = 0)]
    public class RangeWeapon : Base.Weapon
    {
        [SerializeField] protected GameObject bulletPrefab;
        
        public override void UseEffect(Fighter fighter, FightTarget target)
        {
            var bullet = Instantiate(bulletPrefab, fighter.transform.position, Quaternion.identity).GetComponent<RangeWeaponBullet>();
            bullet.OnHit += OnBulletHit;
            bullet.SetTarget(fighter, target);
        }

        private void OnBulletHit(RangeWeaponBullet bullet)
        {
            bullet.Target.Attack(bullet.Fighter, this);
            Destroy(bullet.gameObject);
        }
    }
}