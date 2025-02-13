using Enums;
using UnityEngine;

namespace Item
{
    public class Items : MonoBehaviour
    {
        private GameObject _prefab; // visuals of the item and extra scripts
        private int _value = 10; // the base value of the item
        private ItemType _itemType = ItemType.Normal; // the type of item
        
        /// <summary>
        /// Set all item data from scriptable object
        /// </summary>
        /// <param name="itemData">Data to duplicate</param>
        public void Initialize(ItemData itemData)
        {
            _prefab = itemData.prefab;
            _value = itemData.value;
            _itemType = itemData.itemType;
        }
    }
}
