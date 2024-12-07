using System;
using System.Collections;
using System.Collections.Generic;
using Fighting.Core;
using Fighting.Hp;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavigationTarget))]
public class FightTarget : MonoBehaviour
{
    public IArmor Armor;
    public NavigationTarget NavigationTarget;
    private IHpHandler hpHandler;

    public Action<Fighter, IWeapon> OnDead; //TODO: лучше перенести в HpHandler

    // TODO: Initializable
    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        NavigationTarget = GetComponent<NavigationTarget>();
        if (!TryGetComponent(out hpHandler))
        {
            Debug.LogError($"No component of IHpHandler on {name}");
        }
    }

    public void Attack(Fighter source, IWeapon weapon)
    {
        float damage = weapon.Damage;
        if (Armor != null)
        {
            damage -= Armor.Protection;
        }

        if (hpHandler.HandleDamage(damage))
        {
            OnDead.Invoke(source, weapon);
        }
    }
}
