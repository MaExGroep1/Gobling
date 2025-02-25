using System.Collections.Generic;
using Item;
using UnityEngine;
using UnityEngine.Serialization;

namespace Customer
{
    public class CustomerBehaviour : MonoBehaviour
    {
        [SerializeField] private Items itemPrefab;
        private LootTable _lootTable; // table of loot the customer can buy outside the shop
        
        private float _greediness; // high value will raise the goblins sell prices and lower buy prices
        private float _satisfaction =  0.5f; // how satisfied the customer is with the user
        private float _trustworthiness; // the percentage of bad items in this customer's lootTable
        
        private int _netWorth; // how much currency the customer has in total
        private int _income; // the amount of currency the customer earns every day 
        private List<Items> _inventory; // all the items the customer has

        public void Initialize(CustomerData customerData)
        {
            _lootTable = customerData.lootTable;
            _greediness = customerData.greediness=
            _trustworthiness = customerData.trustworthiness;
            _netWorth = customerData.netWorth;
            _income = customerData.income;
            for (var i = 0; i < customerData.startInventorySize; i++)
                OnGetNewItem(_lootTable.GetRandomLoot());
        }
        /// <summary>
        /// Instantiate new item on the customer
        /// </summary>
        /// <param name="itemData">The item to instantiate</param>
        private void OnGetNewItem(ItemData itemData)
        {
            var item = Instantiate(itemPrefab, transform, true);
            item.Initialize(itemData);
            _inventory.Add(item);
        }
        
        /// <summary>
        /// The customer tries to sell an item to the user
        /// </summary>
        public async void OnOfferItem()
        {
            var item = _inventory[Random.Range(0, _inventory.Count)];
            // TODO: offer item
            // TODO: await user price
            // TODO: check if customer agrees on price
            // TODO: sell or deny
        }
        
        /// <summary>
        /// The Customer Tries to buy an item from the user
        /// </summary>
        public async void OnTryBuyItem()
        {
            // TODO: get random item form user and offer price
            // TODO: await user price
            // TODO: check if customer agrees on price
            // TODO: sell or deny
        }
    }
}
