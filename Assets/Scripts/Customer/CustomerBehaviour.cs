using System;
using System.Collections;
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
        private float _speed; // the speed at which the customer moves
        private float _turnSpeed; // the speed at which the customer turns

        
        private int _netWorth; // how much currency the customer has in total
        private int _income; // the amount of currency the customer earns every day 
        private readonly List<Items> _inventory = new(); // all the items the customer has
        
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
            _speed = customerData.speed;
            _turnSpeed = customerData.turnSpeed;
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
        /// <param name="path"></param>
        /// <param name="onComplete"></param>
        public void EnterShop(Transform[] path, Action onComplete)
        {
            transform.position = path[0].position;
            gameObject.SetActive(true);
            StartCoroutine(MoveCustomer(path,onComplete));
        }

        /// <summary>
        /// Enter the shop to barter with the player
        /// </summary>
        /// <param name="path"></param>
        /// <param name="onComplete"></param>
        public void ExitShop(Transform[] path, Action onComplete)
        {
            transform.position = path[0].position;
            gameObject.SetActive(true);
            StartCoroutine(MoveCustomer(path,onComplete));
        }

        /// <summary>
        /// Leave the shop after bartering
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recall"></param>
        private IEnumerator MoveCustomer(Transform[] path, Action recall)
        {
            for (var index = 1; index < path.Length; index++)
            {
                var point = path[index];
                var distance = Vector3.Distance(transform.position, point.position);
                var direction = point.position - transform.position;
                var rotation = Quaternion.LookRotation(direction).eulerAngles;
                //LeanTween.rotateY(gameObject,rotation.y,_turnSpeed).setEase(LeanTweenType.easeInQuad);
                LeanTween.move(gameObject, point, distance / _speed).setEase(LeanTweenType.easeInQuad);
                yield return new WaitWhile(() => LeanTween.isTweening(gameObject));
            }

            recall?.Invoke();
        }
    }
}
