using Enums;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemScriptableObject", order = 1)]
    public class ItemData : ScriptableObject
    {
        public GameObject prefab; // visuals of the item and extra scripts
        public int value = 10; // the base value of the item
        public ItemType itemType = ItemType.Normal; // the type of item
    }
}
