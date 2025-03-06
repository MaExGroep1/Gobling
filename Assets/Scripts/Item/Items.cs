using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace Item
{
    public class Items : MonoBehaviour
    {
        [SerializeField] private MinMax<int> barValues;     // Minimum and Maximum value Percentage
        [SerializeField] private GameObject itemParent;
        
        private GameObject _prefab;                         //visuals of the item and extra scripts
        private ItemType _itemType = ItemType.Normal;       //the type of item
        public int value { get; private set; } = 10;        //the base value of the item

        /// <summary>
        /// Set all item data from scriptable object
        /// </summary>
        /// <param name="itemData">Data to duplicate</param>
        /// <param name="itemName">The name of the Item</param>
        public void Initialize(ItemData itemData, string itemName)
        {
            gameObject.name = itemName;
            _prefab = itemData.prefab;
            value = itemData.value;
            _itemType = itemData.itemType;
            barValues = itemData.barValues; 
            Instantiate(itemData.prefab, itemParent.transform);
        }
        
        public MinMax<int> barValue => barValues;
        
    }
}
