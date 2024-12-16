using Fighting.Hp;
using Fighting.Items.Armor;
using Fighting.Items.Weapon.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
    public class UnitInventory : MonoBehaviour, IInitializable
    {
        public Weapon Weapon;
        public Armor Armor;

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

        public void AddItem(InventoryItem inventoryItem)
        {
            if (inventoryItem is Weapon weapon)
            {
                if (Weapon != null)
                {
                    DropItem(Weapon);
                }
                Weapon = weapon;
                _fighter.Weapon = weapon;
                PlayPickupSound(weaponPickupSound); // Воспроизводим звук для оружия
                return;
            }

            if (inventoryItem is Armor armor)
            {
                if (Armor != null)
                {
                    DropItem(Armor);
                }
                Armor = armor;
                _fightTarget.Armor = armor;
                PlayPickupSound(armorPickupSound); // Воспроизводим звук для брони
                return;
            }
        }

        public void DropItem(InventoryItem inventoryItem)
        {
            ItemsFabric.instance.DropItem(this, inventoryItem);
        }

        private void PlayPickupSound(AudioClip sound)
        {
            if (sound != null && audioSource != null)
            {
                audioSource.PlayOneShot(sound); // Воспроизводим указанный звук
            }
        }
    }
}
