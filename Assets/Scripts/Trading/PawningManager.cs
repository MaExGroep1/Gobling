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
        public Action<MinMax<int>, int> OnStartpawn;
        
        public Action<int> OnBid;
        
        public MinMax<int> barValue;

        [SerializeField] private TradingUI tradingUIManager;
        
        
        //TEMP
        [SerializeField] private ItemData itemData; // The item data reference
        
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
        
        public void CheckBid(int bid)
        {
            OnBid?.Invoke(bid);
            
            //!TODO Implement checking algo
            
            tradingUIManager.OnBidFinish();
            DayLoopEvents.Instance.CustomerLeave?.Invoke();
        }
    }
}
