using System;
using System.Collections.Generic;
using DayLoop;
using Item;
using UnityEngine;
using Util;
using Random = UnityEngine.Random;

namespace User
{
    public class UserData : Singleton<UserData>
    {
        private readonly List<Items> _inventory = new List<Items>(); // a list of Items that the player has
        [SerializeField] private int startMoney;
        public Items randomItem => _inventory[Random.Range(0, _inventory.Count)]; // returns a random item from the user
        public int inventoryCount => _inventory.Count; // the amount of items the player has
        public int netWorth { get; private set; }

        public Action<int> OnCurrencyChanged; // gets called when the player spends or earns currency

        private void Start()
        {
            ChangeNetWorth(startMoney);
        }

        /// <summary>
        /// Changes the current net worth by adding "change" to it
        /// </summary>
        /// <param name="change"> Amount to add to netWorth</param>
        public void ChangeNetWorth(int change)
        {
            netWorth += change;
            if (netWorth > 0)
            {
                OnCurrencyChanged?.Invoke(netWorth);
                return;
            }
            DayLoopEvents.Instance.GameOver?.Invoke();
        }

        public void SellItem(Items item, int price)
        {
            _inventory.Remove(item);
            ChangeNetWorth(price);
        }

        public void BuyItem(Items item, int price)
        {
            _inventory.Add(item);
            ChangeNetWorth(-price);
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
