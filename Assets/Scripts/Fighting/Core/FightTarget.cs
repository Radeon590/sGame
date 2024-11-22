using System;
using System.Collections;
using System.Collections.Generic;
using Fighting.Core;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavigationTarget))]
public class FightTarget : MonoBehaviour
{
    private IArmor _armor;
    public NavigationTarget NavigationTarget;
    [SerializeField] private IHpHandler hpHandler;

    // TODO: Initializable
    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        NavigationTarget = GetComponent<NavigationTarget>();
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
