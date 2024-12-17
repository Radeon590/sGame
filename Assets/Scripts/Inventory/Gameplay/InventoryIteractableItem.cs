using UnityEngine.Serialization;

namespace Inventory.Gameplay
{
    public class InventoryIteractableItem : Interactable
    {
        [FormerlySerializedAs("InventoryItem")] public Item item;

        public override void Interact(Interactor interactor)
        {
            base.Interact(interactor);
            if (interactor.TryGetComponent(out UnitInventory inventory))
            {
                inventory.AddItem(item);
            }
            Destroy(gameObject);
        }
    }
}