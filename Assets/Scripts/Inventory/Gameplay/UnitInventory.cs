using System;
using Fighting.Items.Armor;
using Fighting.Items.Weapon.Base;
using Inventory.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Inventory.Gameplay
{
    public class UnitInventory : MonoBehaviour, IInitializable
    {
        [NonSerialized] public InventoryUiUnit InventoryUnit;
        
        public Weapon Weapon;
        public Armor Armor;
        public Action<Item> OnAddItem;

        [SerializeField] private AudioClip weaponPickupSound;
        [SerializeField] private AudioClip armorPickupSound;
        private AudioSource audioSource;

        private Fighter _fighter;
        private FightTarget _fightTarget;

        public bool IsInitializationOnStartRequired => true;
        public UnityEvent OnInitialized { get; }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        public void Initialize()
        {
            _fightTarget = GetComponent<FightTarget>();
            _fightTarget.Armor = Armor;
            _fighter = GetComponent<Fighter>();
            _fighter.Weapon = Weapon;
        }

        public void AddItem(Item item)
        {
            if (item is Weapon weapon)
            {
                if (Weapon != null)
                {
                    DropItem(Weapon);
                }
                Weapon = weapon;
                _fighter.Weapon = weapon;
                PlayPickupSound(weaponPickupSound); // Воспроизводим звук для оружия
            }

            if (item is Armor armor)
            {
                if (Armor != null)
                {
                    DropItem(Armor);
                }
                Armor = armor;
                _fightTarget.Armor = armor;
                PlayPickupSound(armorPickupSound); // Воспроизводим звук для брони
            }
            
            OnAddItem?.Invoke(item);
        }

        public void DropItem(Item item)
        {
            ItemsFabric.instance.DropItem(this, item);
        }

        private void PlayPickupSound(AudioClip sound)
        {
            if (sound != null && audioSource != null)
            {
                audioSource.PlayOneShot(sound); // Воспроизводим указанный звук
            }
        }

        private void OnDestroy()
        {
            if (InventoryUnit != null)
            {
                Destroy(InventoryUnit.gameObject);
            }
        }
    }
}
