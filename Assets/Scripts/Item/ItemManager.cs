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
        
        [SerializeField] private Items itemPrefab;
        [SerializeField] private Transform itemParent;
        [SerializeField] private Transform itemCounterLocation;
        [SerializeField] private Transform itemCustomerLocation;
        [SerializeField] private Transform itemPlayerLocation;
        
        private readonly List<Items> _allItems = new();

        public Vector3 ItemCounterJumpLocation
        {
            get => Instance.itemCounterLocation.position;
            private set => Instance.itemCounterLocation.position = value;
        }
        
        public static Vector3 ItemCustomerJumpLocation
        {
            get => Instance.itemCustomerLocation.position;
            private set => Instance.itemCustomerLocation.position = value;
        }
        
        public static Vector3 ItemPlayerJumpLocation
        {
            get => Instance.itemPlayerLocation.position;
            private set => Instance.itemPlayerLocation.position = value;
        }

        public static Action<Items, Vector3, Vector3> OnEnableAndJump;
        public static Action<Items, Vector3, Vector3> OnJumpAndDisable;

        public Items InstantiateItem(ItemData itemData, string itemName)
        {
            Items myItemPrefab;
            var item = Instantiate(itemPrefab, itemParent, true);
            item.Initialize(itemData, itemName);
            _allItems.Add(item);
            return item;
        }
        

        public void ItemEnableAndJump(Items item, Vector3 jumpPosition, Vector3 startPosition = default)
        {
            OnEnableAndJump?.Invoke(item, startPosition, jumpPosition);
        }
        
        public static void ItemJumpAndDisable(Items item, Vector3 jumpPosition, Vector3 startPosition = default)
        {
            OnJumpAndDisable?.Invoke(item, startPosition, jumpPosition);
        }
    }
}