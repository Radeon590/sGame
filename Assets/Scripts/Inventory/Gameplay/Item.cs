using UnityEngine;

namespace Inventory.Gameplay
{
    public abstract class Item : ScriptableObject, IInventoryUiItem
    {
        public GameObject InteractableItemPrefab;
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
    }
}