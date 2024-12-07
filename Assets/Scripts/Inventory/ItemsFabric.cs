using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Inventory
{
    public class ItemsFabric : MonoBehaviour, IInitializable
    {
        public static ItemsFabric instance;
        
        [SerializeField] private Transform itemsParent;

        public void DropItem(UnitInventory unitInventory, InventoryItem item)
        {
            InventoryIteractableItem itemObject = Instantiate(item.InteractableItemPrefab, itemsParent).GetComponent<InventoryIteractableItem>();
            itemObject.transform.position = unitInventory.transform.position;
            itemObject.InventoryItem = item;
        }

        public bool IsInitializationOnStartRequired => true;
        public UnityEvent OnInitialized { get; }
        public void Initialize()
        {
            instance = this;
        }
    }
}