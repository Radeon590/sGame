using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    private IWeapon _weapon;
    public IWeapon Weapon
    {
        set => _weapon = value;
        get => _weapon;
    }
    public FightTarget Target;
    private bool _isFighting;
    public bool IsFighting => _isFighting;

    private bool _isCooldownPassed = true;

    // Звук атаки
    [SerializeField] private AudioClip attackSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetTarget(FightTarget target)
    {
        Target = target;
        _isFighting = true;
    }

    public void CancelFight()
    {
        Target = null;
        _isFighting = false;
    }

    private void FixedUpdate()
    {
        if (Target != null && _isCooldownPassed)
        {
            if (Vector2.Distance(transform.position, Target.transform.position) <= Weapon.Range)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        Weapon.UseEffect(this, Target);
        PlayAttackSound();
        StartCooldown();
    }

    private void PlayAttackSound()
    {
        if (attackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }

    private void StartCooldown()
    {
        _isCooldownPassed = false;
        StartCoroutine(CoolDownCoroutine());
    }

    private IEnumerator CoolDownCoroutine()
    {
        yield return new WaitForSeconds(_weapon.Rate);
        _isCooldownPassed = true;
    }
}
