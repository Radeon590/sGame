using Inventory;
using Inventory.Gameplay;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fighting.Items.Armor
{
    [CreateAssetMenu(fileName = "Armor", menuName = "FightingItems/Armor", order = 0)]
    public class Armor : Item, IArmor
    {
        [SerializeField] protected float protection;
        public float Protection => protection;
    }
}