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

        private readonly List<Items> _allItems = new();

        public Items InstantiateItem(ItemData itemData, string itemName)
        {
            var item = Instantiate(itemPrefab, itemParent, true);
            item.Initialize(itemData, itemName);
            _allItems.Add(item);
            return item;
        }
    }
}