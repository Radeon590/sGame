using System.Collections.Generic;
using Inventory.Gameplay;
using UnityEngine;

namespace Inventory.UI
{
    public class InventoryUi : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryUiUnitPrefab;
        [SerializeField] private Transform inventoryPanel;

        public void SetInventoryUnits(List<UnitInventory> inventoryUnits)
        {
            for (int i = 0; i < inventoryPanel.childCount; i++)
            {
                Destroy(inventoryPanel.GetChild(i).gameObject);
            }

            foreach (var inventoryUnit in inventoryUnits)
            {
                var unitUi = Instantiate(inventoryUiUnitPrefab, inventoryPanel).GetComponent<InventoryUiUnit>();
                unitUi.SetUp(inventoryUnit);
            }
        }
    }
}
