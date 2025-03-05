using System;
using System.Collections.Generic;
using Item;
using UnityEngine;
using Util;
using Random = UnityEngine.Random;

namespace User
{
    public class UserData : Singleton<UserData>
    {
        private static int thisNetWorth; // the amount of currency the player has

        private List<Items> _inventory = new List<Items>(); // a list of Items that the player has
        public Items randomItem => _inventory[Random.Range(0, _inventory.Count)]; // returns a random item from the user
        public int inventoryCount => _inventory.Count; // the amount of items the player has
        public int netWorth => thisNetWorth; // the amount of currency the player has
        
        public Action<int> OnCurrencyChanged; // gets called when the player spends or earns currency
        
        /// <summary>
        /// Changes the current net worth by adding "change" to it
        /// </summary>
        /// <param name="change"> Amount to add to netWorth</param>
        public void ChangeNetWorth(int change)
        {
            thisNetWorth += change;
            OnCurrencyChanged?.Invoke(thisNetWorth);
        }
        
        /// <summary>
        /// Add "item" to Inventory
        /// </summary>
        /// <param name="item">Item to add</param>
        public void AddItemToInventory(Items item) => _inventory.Add(item);

        /// <summary>
        /// Remove "item" to Inventory
        /// </summary>
        /// <param name="item">Item to remove</param>
        public bool RemoveItemFromInventory(Items item)
        {
            if (!_inventory.Contains(item)) return false;
            _inventory.Remove(item);
            return true;
        }
    }
}
