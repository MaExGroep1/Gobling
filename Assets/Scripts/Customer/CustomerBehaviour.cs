using System;
using System.Collections.Generic;
using Item;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Customer
{
    public class CustomerBehaviour : MonoBehaviour
    {
        private LootTable _lootTable; // table of loot the customer can buy outside the shop
        
        private float _greediness; // high value will raise the goblins sell prices and lower buy prices
        private float _satisfaction =  0.5f; // how satisfied the customer is with the user
        private float _trustworthiness; // the percentage of bad items in this customer's lootTable
        
        private int _netWorth; // how much currency the customer has in total
        private int _income; // the amount of currency the customer earns every day 
        private readonly List<Items> _inventory = new(); // all the items the customer has

        public Action OnExitShop;

        /// <summary>
        /// Transfer all data from the customer data scriptable object to this script
        /// </summary>
        /// <param name="customerData">The Target Data to copy</param>
        public void Initialize(CustomerData customerData)
        {
            Instantiate(customerData.prefab, transform);
            gameObject.name = customerData.name;
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
            var item = ItemManager.Instance.InstantiateItem(itemData,$"{gameObject.name}.{itemData.name}");
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
        /// <summary>
        /// Enter the shop to barter with the player
        /// </summary>
        /// <param name="customerEntryPoint">The position where the customer starts</param>
        /// <param name="customerTradePoint">The target position of the customer</param>
        /// <param name="speed">The speed at which the customer moves</param>
        public void EnterShop(Transform customerEntryPoint,Transform customerTradePoint, float speed)
        {
            transform.position = customerEntryPoint.position;
            gameObject.SetActive(true);
            var distance = Vector3.Distance(transform.position, customerTradePoint.position);
            LeanTween.move(gameObject, customerTradePoint, distance / speed).setEase(LeanTweenType.easeOutQuad);
        }
        /// <summary>
        /// Leave the shop after bartering
        /// </summary>
        /// <param name="customerExitPoint">The position to move to to exit the shop</param>
        /// <param name="speed">The speed at which the customer moves</param>
        public void ExitShop(Transform customerExitPoint, float speed)
        {
            var distance = Vector3.Distance(transform.position, customerExitPoint.position);
            LeanTween.move(gameObject, customerExitPoint, distance / speed).setEase(LeanTweenType.easeInQuad).setOnComplete(OnShopExited);
        }
        /// <summary>
        /// Gets called when the customer leaves the shop. Makes the customer inactive
        /// </summary>
        private void OnShopExited()
        {
            OnExitShop?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
