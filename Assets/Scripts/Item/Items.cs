using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace Item
{
    public class Items : MonoBehaviour
    {
        public string ItemName;
        [SerializeField] private GameObject visuals;        //the parent GameObject of all visuals
        [SerializeField] private float activationSpeed;     //the speed at which the item activates
        [SerializeField] private float deactivationSpeed;   //the speed at which the item deactivates
        [SerializeField] private float jumpXSpeed;          //x-axis move speed for JumpToPosition()
        [SerializeField] private float jumpYSpeed;          //y-axis move speed for JumpToPosition()
        [SerializeField] private float jumpHeight;          //jump height for JumpToPosition()
        [SerializeField] private MinMax<int> barValues;     // Minimum and Maximum value Percentage
        [SerializeField] private GameObject itemParent;     // Parent of the item visuals and data
        
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
            ItemName = itemData.name;
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
