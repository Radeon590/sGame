using System;
using System.Collections;
using System.Collections.Generic;
using Fighting.Core;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavigationTarget), typeof(IHpHandler))]
public class FightTarget : MonoBehaviour
{
    public IArmor Armor;
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
        float damage = weapon.Damage - Armor.Armor;
        hpHandler.HandleDamage(damage);
    }
}
