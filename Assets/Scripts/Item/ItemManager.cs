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
        [SerializeField] private Items itemPrefab;          // The item template to instantiate
        [SerializeField] private Transform itemParent;      // The transform to parent the items to

        private readonly List<Items> _allItems = new();     // List of all items in play
        
        /// <summary>
        /// Instantiates the item for the user or customer
        /// </summary>
        /// <param name="itemData"></param>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public Items InstantiateItem(ItemData itemData, string itemName)
        {
            var item = Instantiate(itemPrefab, itemParent, true);
            item.Initialize(itemData, itemName);
            _allItems.Add(item);
            return item;
        }
    }
}