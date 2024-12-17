using UnityEngine;
using UnityEngine.Events;

namespace Inventory.Gameplay
{
    public class ItemsFabric : MonoBehaviour, IInitializable
    {
        public static ItemsFabric instance;
        
        [SerializeField] private Transform itemsParent;

        public void DropItem(UnitInventory unitInventory, Item item)
        {
            InventoryIteractableItem itemObject = Instantiate(item.InteractableItemPrefab, itemsParent).GetComponent<InventoryIteractableItem>();
            itemObject.transform.position = unitInventory.transform.position;
            itemObject.item = item;
        }

        public bool IsInitializationOnStartRequired => true;
        public UnityEvent OnInitialized { get; }
        public void Initialize()
        {
            instance = this;
        }
    }
}