using System;
using System.Collections;
using Fighting.Core;
using UnityEngine;

namespace Fighting.Hp
{
    public class HpHandler : MonoBehaviour, IHpHandler
    {
        [SerializeField] protected float hp;
        public float Hp => hp;
        public Action OnDead;
        public bool IsDead => hp <= 0;

        [SerializeField] private AudioClip deathSound;
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        public bool HandleDamage(float damage)
        {
            hp -= damage;
            if (hp <= 0)
            {
                OnDead?.Invoke();
                PlayDeathSound();
                StartCoroutine(DestroyAfterSound());
                return true;
            }
            return false;
        }

        private void PlayDeathSound()
        {
            if (deathSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(deathSound);
            }
        }

        private IEnumerator DestroyAfterSound()
        {
            if (audioSource.isPlaying)
            {
                yield return new WaitForSeconds(deathSound.length);
            }
            Destroy(gameObject);
        }
    }
}
