using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace Item
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemScriptableObject", order = 1)]
    public class ItemData : ScriptableObject
    {
        public GameObject prefab;                   // visuals of the item and extra scripts
        public int value = 10;                      // the base value of the item
        public ItemType itemType = ItemType.Normal; // the type of item

        public MinMax<int> valuePercentage;         // Minimum and Maximum value Percentage
        
    }
}
