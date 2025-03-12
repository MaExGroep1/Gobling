using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Item;
using Trading;
using Unity.Mathematics;
using UnityEngine;
using User;
using Random = UnityEngine.Random;

namespace Customer
{
    public class CustomerBehaviour : MonoBehaviour
    {
        private bool _isRotating;
        private LootTable _lootTable;                       // table of loot the customer can buy outside the shop
        private float _greediness;                          // high value will raise the goblins sell prices and lower buy prices
        private float _satisfaction =  0.5f;                // how satisfied the customer is with the user
        private float _trustworthiness;                     // the percentage of bad items in this customer's lootTable
        private float _speed;                               // the speed at which the customer moves
        private float _turnSpeed;                           // the speed at which the customer turns
        private Coroutine _rotationCoroutine;               // the coroutine of the customer rotating while walking
        private CustomerAnimations _animator;                // the animator of the customer
        private int _income;                                // the amount of currency the customer earns every day 
        private readonly List<Items> _inventory = new();    // all the items the customer has
        public int netWorth { get; private set; }           // how much currency the customer has in total


        /// <summary>
        /// Transfer all data from the customer data scriptable object to this script
        /// </summary>
        /// <param name="customerData">The Target Data to copy</param>
        public void Initialize(CustomerData customerData)
        {
            _animator = Instantiate(customerData.customerAnimations, transform);
            gameObject.name = customerData.name;
            _lootTable = customerData.lootTable;
            _greediness = customerData.greediness;
            _trustworthiness = customerData.trustworthiness;
            netWorth = customerData.netWorth;
            _income = customerData.income;
            _speed = customerData.speed;
            _turnSpeed = customerData.turnSpeed;
            for (var i = 0; i < customerData.startInventorySize; i++)
                OnGetNewItem(_lootTable.GetRandomLoot());
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
            _animator.SetSpeedMultiplierFloat(_speed);
            transform.rotation = Quaternion.Euler(rotation);

            transform.position = path[0].position;
            gameObject.SetActive(true);
            MoveCustomer(path,() => StartCoroutine(OnAtCounter(onComplete)));
        }

        /// <summary>
        /// Enter the shop to barter with the player
        /// </summary>
        /// <param name="path"></param>
        /// <param name="onComplete"></param>
        public void ExitShop(Transform[] path, Action onComplete)
        {
            _animator.TriggerLeaveCounter();
            transform.position = path[0].position;
            MoveCustomer(path,onComplete);
        }

        /// <summary>
        /// Calculates the offset from the default price the customer will aks for
        /// </summary>
        /// <param name="itemValue">The value of the item</param>
        /// <returns>The offset to add or remove from the item</returns>
        public int GetOfferOffset(int itemValue) => (int)(itemValue * (1 - _greediness) * (1 - _satisfaction) + itemValue * 0.05f);
        
        /// <summary>
        /// Updates the satisfaction of the customer
        /// </summary>
        /// <param name="increase">Weather to increase or decrease</param>
        /// <param name="multiplier">The multiplier</param>
        public void UpdateSatisfaction(bool increase, float multiplier)
        {
            _satisfaction += increase ? _satisfaction * _satisfaction * multiplier : -_satisfaction * (1 -_satisfaction) * multiplier;
            _satisfaction = Mathf.Clamp01(_satisfaction);
        }

        /// <summary>
        /// Sees if the customer is still interested in bartering
        /// </summary>
        /// <param name="newBid">The offer of the user</param>
        /// <param name="originalOffer">The offer of the customer</param>
        /// <param name="isBuying">True means the customer is buying false means the customer is selling</param>
        /// <returns>Weather the customer will stay</returns>
        public bool IsInterested(int newBid, int originalOffer, bool isBuying) => isBuying ? 
            newBid > originalOffer - CalculateWiggleRoom(originalOffer) * 10 * _satisfaction : 
            newBid < originalOffer + CalculateWiggleRoom(originalOffer) * 10 * _satisfaction;
        
        /// <summary>
        /// Sees if the customer wants to buy or sell the item
        /// </summary>
        /// <param name="newBid">The offer of the user</param>
        /// <param name="originalOffer">The offer of the customer</param>
        /// <param name="isBuying">True means the customer is buying false means the customer is selling</param>
        /// <returns>Weather the wants to buy or sell the item</returns>
        public bool WillBuy(int newBid, int originalOffer, bool isBuying) => isBuying ? 
                newBid > originalOffer - CalculateWiggleRoom(originalOffer) * _greediness: 
                newBid < originalOffer + CalculateWiggleRoom(originalOffer) * _greediness;
        
        /// <summary>
        /// Calculates the new offer of the customer
        /// </summary>
        /// <param name="userBid">The offer of the user</param>
        /// <param name="originalOffer">The offer of the customer</param>
        /// <returns>The new offer of the customer</returns>
        public int MakeNewOffer(int userBid, int originalOffer) => Mathf.RoundToInt( Mathf.Lerp(originalOffer, userBid, (1-_greediness) * _satisfaction));

        /// <summary>
        /// Checks if customer has the item then removes it if so
        /// </summary>
        /// <param name="item">The item to remove</param>
        /// <param name="cost">The cost of the item</param>
        /// <returns>If the customer has the item</returns>
        public bool RemoveItem(Items item, int cost)
        {
            if (!_inventory.Contains(item)) return false;
            _inventory.Remove(item);
            netWorth += cost;
            return true;
        }

        /// <summary>
        /// Adds item to your inventory
        /// </summary>
        /// <param name="item">The item to add to the customer</param>
        /// <param name="cost">The cost of the item</param>
        public void AddItem(Items item, int cost)
        {
            netWorth -= cost;
            item.name = $"{gameObject.name}.{item.ItemName}";
            _inventory.Add(item);
        }
        
        /// <summary>
        /// Wait until the item can jump to the hand then wait until the customer can leave
        /// </summary>
        /// <returns></returns>
        public IEnumerator TakeItem()
        {
            _animator.CantLeaveShop();
            _animator.CantJump();
            _animator.TriggerGiveTake();
            yield return new WaitUntil(() => _animator.itemCanJump);
            // TODO: Add jump here
            yield return new WaitUntil(() => _animator.canLeaveShop);
        }
        /// <summary>
        /// Calculate the wiggle room of an item
        /// </summary>
        /// <param name="bid">The price of the item</param>
        /// <returns>The wiggle room of the item</returns>
        private int CalculateWiggleRoom(int bid) => Mathf.RoundToInt(bid * (1 - _greediness));
        
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
            var item = _inventory[Random.Range(0, _inventory.Count - 1)];
            
            _animator.TriggerGiveTake();

            StartCoroutine(ItemAnimation());
            
            PawningManager.Instance.OfferUserItem(item,item.value + GetOfferOffset(item.value),this);
        }
        
        /// <summary>
        /// The Customer Tries to buy an item from the user
        /// </summary>
        private void OnTryBuyItem()
        {
            PawningManager.Instance.RequestUserItem(this);
        }

        /// <summary>
        /// Makes the customer chose weather to sell or buy.
        /// Depending on the customers inventory size and the players inventory size
        /// </summary>
        /// <param name="recall">Action to call after choosing the to buy or sell</param>
        private IEnumerator OnAtCounter(Action recall)
        {
            _animator.TriggerAtCounter();
            
            yield return new WaitUntil(() => !_isRotating);
            
            var validItems = _inventory.Count + UserData.Instance.inventoryCount + 1;
            if (_inventory.Count < Random.Range(1, validItems))
                OnTryBuyItem();
            else
                OnOfferItem();
            recall?.Invoke();
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

            _isRotating = true;
            while (rotationTime < duration)
            {
                rotationTime += Time.deltaTime * _turnSpeed;
                var t = rotationTime / duration;
                t = 1 - Mathf.Pow(1 - t, 4); 

                var newY = Mathf.LerpAngle(startAngle, targetAngle, t);
                transform.rotation = Quaternion.Euler(0, newY, 0);

                yield return null;
            }
            _isRotating = false;
            transform.rotation = targetRotation;
        }
                
        /// <summary>
        /// Leave the shop after bartering
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recall"></param>
        private async void MoveCustomer(Transform[] path, Action recall)
        {
            _animator.SetSpeedMultiplierFloat(_speed);
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
        }
        
        /// <summary>
        /// Makes the customer wait until it can jump to the table
        /// </summary>
        /// <returns></returns>
        private IEnumerator ItemAnimation()
        {
            _animator.CantJump();
            yield return new WaitUntil(() => _animator.itemCanJump);
            //TODO: add item jump
        }
    }
}
