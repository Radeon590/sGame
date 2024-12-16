using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    public class InventoryIteractableItem : Interactable
    {
        public InventoryItem InventoryItem;

        public override void Interact(Interactor interactor)
        {
            base.Interact(interactor);
            if (interactor.TryGetComponent(out UnitInventory inventory))
            {
                inventory.AddItem(InventoryItem);
            }
            Destroy(gameObject);
        }
    }
}