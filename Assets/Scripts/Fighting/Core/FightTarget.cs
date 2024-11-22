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
    private IArmor _armor;
    public NavigationTarget NavigationTarget;
    private IHpHandler hpHandler;

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

    public void Attack(IWeapon weapon)
    {
        float damage = weapon.Damage;
        if (_armor != null)
        {
            damage -= _armor.Protection;
        }
        hpHandler.HandleDamage(damage);
    }
}
