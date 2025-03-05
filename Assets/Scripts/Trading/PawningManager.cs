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
        
        private CustomerBehaviour _currentCustomer; // the customer that is being served
        private int _offerAmount;
        private MinMax<int> _acceptableBidRange;

        [SerializeField] private TradingUI tradingUIManager; // Reference to the UI manager for trading
        
        
        
        
        /// <summary>
        /// The customer offers the player an item for a price
        /// </summary>
        /// <param name="item">The item for sale</param>
        /// <param name="offerAmount">The initial amount the customer wants for it</param>
        /// <param name="customer">The new customer to serve</param>
        public void OfferUserItem(Items item,int offerAmount,CustomerBehaviour customer)
        {
            _currentCustomer = customer;
            _offerAmount = offerAmount;
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
            //!TODO make item.value calculation
            tradingUIManager.OnStartpawn(item.barValue, item.value);
        }
        
        /// <summary>
        /// Processes the bid made by the player and determines the outcome
        /// </summary>
        /// <param name="bid">The bid amount placed by the player</param>
        public void CheckBid(int bid)
        {
            OnBid?.Invoke(bid);
            var acceptableBid = _offerAmount;
            
            //!TODO Implement checking algo
            
            tradingUIManager.OnBidFinish();
            DayLoopEvents.Instance.CustomerLeave?.Invoke();
        }

        private bool IsBidAcceptable(MinMax<int> barValue, int greed, int offerAmount)
        {
            int wiggleRoom = (barValue.max - barValue.min) * (1 - greed);
            
            float acceptableMin = Math.Max(barValue.min, offerAmount - wiggleRoom / 2);
            float acceptableMax = Math.Min(barValue.max, offerAmount + wiggleRoom / 2);

            return offerAmount >= acceptableMin && offerAmount <= acceptableMax;
        }
    }
}
