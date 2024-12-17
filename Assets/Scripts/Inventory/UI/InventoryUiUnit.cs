using System;
using Fighting.Items.Armor;
using Fighting.Items.Weapon.Base;
using Inventory.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class InventoryUiUnit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image unitIconImage;
        [SerializeField] private Image weaponImage;
        [SerializeField] private Image armorImage;
        [SerializeField] private GameObject popUpPanel;

        private UnitInventory _unitInventory;
        
        public void SetUp(UnitInventory unitInventory)
        {
            _unitInventory = unitInventory;
            _unitInventory.InventoryUnit = this;
            _unitInventory.OnAddItem += OnUnitAddItem;
            
            popUpPanel.SetActive(false);
            unitIconImage.sprite = unitInventory.GetComponent<SpriteRenderer>().sprite;
            
            if (unitInventory.Weapon != null)
            {
                SetIcon(weaponImage, unitInventory.Weapon.Icon);
            }
            else
            {
                weaponImage.gameObject.SetActive(false);
            }

            if (unitInventory.Armor != null)
            {
                SetIcon(armorImage, unitInventory.Armor.Icon);
            }
            else
            {
                armorImage.gameObject.SetActive(false);
            }
        }

        public void OnUnitAddItem(Item item)
        {
            if (item is Weapon)
            {
                SetIcon(weaponImage, item.Icon);
                return;
            }

            if (item is Armor armor)
            {
                SetIcon(armorImage, item.Icon);
                return;
            }
        }

        private void SetIcon(Image image, Sprite icon)
        {
            image.sprite = icon;
            image.gameObject.SetActive(true);
        }
        
        private void OnDestroy()
        {
            _unitInventory.OnAddItem -= OnUnitAddItem;
            _unitInventory.InventoryUnit = null;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            popUpPanel.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            popUpPanel.SetActive(false);
        }
    }
}