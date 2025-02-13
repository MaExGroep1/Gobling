using System.Collections.Generic;
using Item;
using UnityEngine;
using Util;

namespace User
{
    public class UserData : Singleton<UserData>
    {
        private static int netWorth; // The amount of currency the player has
        public int NetWorth => netWorth;

        private List<Items> _inventory; // A list of Items that the player has
        public List<Items> Inventory => _inventory;
        
        /// <summary>
        /// Changes the current net worth by adding "change" to it
        /// </summary>
        /// <param name="change"> Amount to add to netWorth</param>
        public void ChangeNetWorth(int change) => netWorth += change;
        
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
