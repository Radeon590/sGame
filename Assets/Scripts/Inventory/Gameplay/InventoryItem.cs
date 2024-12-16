using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    public abstract class InventoryItem : ScriptableObject, IUIInventoryHands
    {
        public GameObject InteractableItemPrefab;
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
    }
}