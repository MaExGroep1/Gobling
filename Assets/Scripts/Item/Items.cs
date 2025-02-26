using Enums;
using UnityEngine;
using Util;

namespace Item
{
    public class Items : MonoBehaviour
    {
        private GameObject _prefab;                     // visuals of the item and extra scripts
        private int _value = 10;                        // The base value of the item
        private ItemType _itemType = ItemType.Normal;   // The type of item
        private MinMax<int> _valuePercentage;           // Minimum and Maximum value Percentage
        
        /// <summary>
        /// Set all item data from scriptable object
        /// </summary>
        /// <param name="itemData">Data to duplicate</param>
        public void Initialize(ItemData itemData)
        {
            _prefab = itemData.prefab;
            _value = itemData.value;
            _itemType = itemData.itemType;
            _valuePercentage = itemData.valuePercentage;
        }
        
        /// <summary>
        /// Calculates min and max percentage values based on `_value`.
        /// </summary>
        /// <returns>returns MinMax(int) with calculated values.</returns>
        public MinMax<int> CalculateValuePercent() => new MinMax<int>(_value / _valuePercentage.min, _value / _valuePercentage.max);
    }
}
