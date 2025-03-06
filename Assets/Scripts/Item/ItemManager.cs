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
        public static ItemManager Instance { get; private set; }
        
        [SerializeField] private Items itemPrefab;
        [SerializeField] private Transform itemParent;
        [SerializeField] private Transform itemCounterJumpLocation;
        [SerializeField] private Transform itemCustomerJumpLocation;
        [SerializeField] private Transform itemPlayerJumpLocation;
        
        private readonly List<Items> _allItems = new();

        public static Vector3 ItemCounterJumpLocation
        {
            get => Instance.itemCounterJumpLocation.position;
            private set => Instance.itemCounterJumpLocation.position = value;
        }
        
        public static Vector3 ItemCustomerJumpLocation
        {
            get => Instance.itemCustomerJumpLocation.position;
            private set => Instance.itemCustomerJumpLocation.position = value;
        }
        
        public static Vector3 ItemPlayerJumpLocation
        {
            get => Instance.itemPlayerJumpLocation.position;
            private set => Instance.itemPlayerJumpLocation.position = value;
        }

        public static Action<Items, Vector3> OnEnableAndJump;
        public static Action<Items, Vector3> OnJumpAndDisable;

        protected override void Awake()
        {
            base.Awake();
            
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public Items InstantiateItem(ItemData itemData, string itemName)
        {
            var item = Instantiate(itemPrefab, itemParent, true);
            item.Initialize(itemData, itemName);
            _allItems.Add(item);
            return item;
        }
        

        public static void ItemEnableAndJump(Items item, Vector3 jumpPosition)
        {
            OnEnableAndJump?.Invoke(item, jumpPosition);
        }
        
        public static void ItemJumpAndDisable(Items item, Vector3 jumpPosition)
        {
            OnJumpAndDisable?.Invoke(item, jumpPosition);
        }
    }
}