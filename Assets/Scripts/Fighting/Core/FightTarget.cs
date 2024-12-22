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

    private void Start()
    {
        try
        {
            SetUp();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in FightTarget.Start: {ex.Message}");
        }
    }

    private void SetUp()
    {
        try
        {
            NavigationTarget = GetComponent<NavigationTarget>();
            if (!TryGetComponent(out hpHandler))
            {
                Debug.LogError($"No component of IHpHandler on {name}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in FightTarget.SetUp: {ex.Message}");
        }
    }

    public void Attack(Fighter source, IWeapon weapon)
    {
        try
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Source fighter is null in FightTarget.Attack.");
            }

            if (weapon == null)
            {
                throw new ArgumentNullException(nameof(weapon), "Weapon is null in FightTarget.Attack.");
            }

            float damage = weapon.Damage;
            if (Armor != null)
            {
                damage -= Armor.Protection;
            }

            if (hpHandler == null)
            {
                throw new NullReferenceException("hpHandler is null in FightTarget.Attack.");
            }

            if (hpHandler.HandleDamage(damage))
            {
                if (OnDead != null)
                {
                    OnDead.Invoke(source, weapon);
                }
                else
                {
                    Debug.LogWarning("OnDead is null, no subscribers to handle death event.");
                }
            }
        }
        catch (ArgumentNullException ex)
        {
            Debug.LogWarning($"ArgumentNullException in FightTarget.Attack: {ex.Message}");
        }
        catch (IndexOutOfRangeException ex)
        {
            Debug.LogWarning($"IndexOutOfRangeException in FightTarget.Attack: {ex.Message}");
        }
        catch (NullReferenceException ex)
        {
            Debug.LogWarning($"NullReferenceException in FightTarget.Attack: {ex.Message}");
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"Unexpected exception in FightTarget.Attack: {ex.Message}");
        }
    }

}
