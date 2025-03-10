using System;
using Customer;
using DayLoop;
using Item;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using User;
using Util;


namespace Trading
{
    public class PawningManager : Singleton<PawningManager>
    {
        public Action<MinMax<int>, int> OnStartpawn; // Event triggered when a pawn transaction starts
        
        public Action<int> OnBid; // Event triggered when a bid is made
        
        public MinMax<int> barValue; // The min and max value range for the pawn bar

        [SerializeField] private TradingUI tradingUIManager; // Reference to the UI manager for trading
        
        private CustomerBehaviour _currentCustomer; // the customer that is being served
        
        
        
        /// <summary>
        /// The customer offers the player an item for a price
        /// </summary>
        /// <param name="item">The item for sale</param>
        /// <param name="offerAmount">The initial amount the customer wants for it</param>
        /// <param name="customer">The new customer to serve</param>
        public void OfferUserItem(Items item,int offerAmount,CustomerBehaviour customer)
        {
            _currentCustomer = customer;
            var itemManager = ItemManager.Instance;
            
            itemManager.ItemEnableAndJump(item, itemManager.ItemCounterJumpLocation, itemManager.ItemCustomerJumpLocation);
            tradingUIManager.OnStartpawn(item.barValue, offerAmount);
            
        }
        /// <summary>
        /// The customer tries to buy an item from the player
        /// </summary>
        /// <param name="customer">The new customer to serve</param>
        /// <returns>The Item to buy</returns>
        public void RequestUserItem(CustomerBehaviour customer)
        {
            _currentCustomer = customer;
            var item = UserData.Instance.randomItem;
            var itemManager = ItemManager.Instance;
            //!TODO make item.value calculation
            
            itemManager.ItemEnableAndJump(item ,itemManager.ItemCounterJumpLocation, itemManager.ItemPlayerJumpLocation);
            tradingUIManager.OnStartpawn(item.barValue, item.value);
            
        }
        
        /// <summary>
        /// Processes the bid made by the player and determines the outcome
        /// </summary>
        /// <param name="bid">The bid amount placed by the player</param>
        public void CheckBid(int bid)
        {
            OnBid?.Invoke(bid);
            
            //!TODO Implement checking algo
            
            tradingUIManager.OnBidFinish();
            DayLoopEvents.Instance.CustomerLeave?.Invoke();
        }
    }
}
