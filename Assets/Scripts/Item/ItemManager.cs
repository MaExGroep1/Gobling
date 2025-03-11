using System;
using System.Collections.Generic;
using Item;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace Item
{
    public class ItemManager : Singleton<ItemManager>
    {
        public Action<Items, Vector3, Vector3> OnEnableAndJump;     //when the item should appear and jump to a location (item, jumpLocation, startLocation)
        public Action<Items, Vector3, Vector3> OnJumpAndDisable;    //when the item should jump to a location and disappear (item, jumpLocation, startLocation)

        [SerializeField] private Items itemPrefab;
        [SerializeField] private Transform itemParent;              //Parent of the item visuals and data
        [SerializeField] private Transform itemCounterLocation;     //The location on the counter where the item can be
        [SerializeField] private Transform itemCustomerLocation;    //The location from where the item will come/go if it is owned by the customer
        [SerializeField] private Transform itemPlayerLocation;      //The location from where the item will come/go if it is owned by the player
        
        private readonly List<Items> _allItems = new();

        public Vector3 ItemCounterJumpLocation
        {
            get => Instance.itemCounterLocation.position;
            private set => Instance.itemCounterLocation.position = value;
        }
        
        public Vector3 ItemCustomerJumpLocation
        {
            get => Instance.itemCustomerLocation.position;
            private set => Instance.itemCustomerLocation.position = value;
        }
        
        public Vector3 ItemPlayerJumpLocation
        {
            get => Instance.itemPlayerLocation.position;
            private set => Instance.itemPlayerLocation.position = value;
        }
        
        public Items InstantiateItem(ItemData itemData, string itemName)
        {
            var item = Instantiate(itemPrefab, itemParent, true);
            item.Initialize(itemData, itemName);
            _allItems.Add(item);
            return item;
        }
        
        /// <summary>
        /// invokes the OnEnableAndJump action
        /// </summary>
        public void ItemEnableAndJump(Items item, Vector3 jumpPosition, Vector3 startPosition = default)
        {
            OnEnableAndJump?.Invoke(item, jumpPosition, startPosition);
        }
        
        /// <summary>
        /// invokes the OnJumpAndDisable action
        /// </summary>
        public void ItemJumpAndDisable(Items item, Vector3 jumpPosition, Vector3 startPosition = default)
        {
            OnJumpAndDisable?.Invoke(item, jumpPosition, startPosition);
        }
    }
}