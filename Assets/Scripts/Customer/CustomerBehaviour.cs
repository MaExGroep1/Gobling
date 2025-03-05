using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Item;
using Trading;
using UnityEngine;
using User;
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
        private Coroutine _rotationCoroutine; // the coroutine of the customer rotating while walking
        private Animator _animator; // the animator of the customer
        
        private int _netWorth; // how much currency the customer has in total
        private int _income; // the amount of currency the customer earns every day 
        private readonly List<Items> _inventory = new(); // all the items the customer has
        
        /// <summary>
        /// Transfer all data from the customer data scriptable object to this script
        /// </summary>
        /// <param name="customerData">The Target Data to copy</param>
        public void Initialize(CustomerData customerData)
        {
            _animator = Instantiate(customerData.prefab, transform);
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
        private void OnOfferItem()
        {
            var item = _inventory[Random.Range(0, _inventory.Count)];
            var offer = item.value / _greediness;
            offer *= _satisfaction;
            
            PawningManager.Instance.OfferUserItem(item,(int)Math.Round(offer),this);
            // TODO: offer item
            
            // TODO: await user price
            
            // TODO: check if customer agrees on price
            
            // TODO: sell or deny
        }
        
        /// <summary>
        /// The Customer Tries to buy an item from the user
        /// </summary>
        private void OnTryBuyItem()
        {
            PawningManager.Instance.RequestUserItem(this);
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
            var direction = path[1].position - transform.position;
            var rotation = Quaternion.LookRotation(direction).eulerAngles;
            _animator.SetFloat("SpeedMultiplier", _speed);
            transform.rotation = Quaternion.Euler(rotation);

            transform.position = path[0].position;
            gameObject.SetActive(true);
            MoveCustomer(path,onComplete);
        }

        /// <summary>
        /// Enter the shop to barter with the player
        /// </summary>
        /// <param name="path"></param>
        /// <param name="onComplete"></param>
        public void ExitShop(Transform[] path, Action onComplete)
        {
            transform.position = path[0].position;
            MoveCustomer(path,onComplete);
        }
        
        /// <summary>
        /// Leave the shop after bartering
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recall"></param>
        private async void MoveCustomer(Transform[] path, Action recall)
        {
            recall += OnAtCounter;
            for (var index = 1; index < path.Length; index++)
            {
                var point = path[index];
                var distance = Vector3.Distance(transform.position, point.position);
                
                if(_rotationCoroutine != null) StopCoroutine(_rotationCoroutine);
                _rotationCoroutine = StartCoroutine(RotateCharacter(point.position));
                LeanTween.move(gameObject, point.position, distance / _speed);
                
                await Task.Delay(10);

                while (LeanTween.isTweening(gameObject))
                    await Task.Delay(10); 
            }
            recall?.Invoke();
            recall -= OnAtCounter;
        }
        
        /// <summary>
        /// Rotate the customer towards the next point in the path easing out quart
        /// </summary>
        /// <param name="rotateTarget">The position to rotate towards</param>
        /// <returns></returns>
        private IEnumerator RotateCharacter(Vector3 rotateTarget)
        {
            var direction = rotateTarget - transform.position;
            var targetRotation = Quaternion.LookRotation(direction);
    
            var startAngle = transform.rotation.eulerAngles.y;
            var targetAngle = targetRotation.eulerAngles.y;
            var rotationTime = 0f;
            const float duration = 1f;

            while (rotationTime < duration)
            {
                rotationTime += Time.deltaTime * _turnSpeed;
                var t = rotationTime / duration;
                t = 1 - Mathf.Pow(1 - t, 4); 

                var newY = Mathf.LerpAngle(startAngle, targetAngle, t);
                transform.rotation = Quaternion.Euler(0, newY, 0);

                yield return null;
            }

            transform.rotation = targetRotation;
        }
        
        private void OnAtCounter()
        {
            var validItems = _inventory.Count + UserData.Instance.inventoryCount;
            if (_inventory.Count < Random.Range(0, validItems))
                OnTryBuyItem();
            else
                OnOfferItem();
        }

    }
}
