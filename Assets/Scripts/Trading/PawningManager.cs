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
        public Action<MinMax<int>, int> OnStartPawn;        // event triggered when a pawn transaction starts
        public Action<int> OnNewBidRound;                   // event when the customer gives a counteroffer
        public Action<int> OnCheckBid;                      // event triggered when a bid is made
        public Action<bool,int> OnFinished;                 // event to trigger when the customer is done pawing
        
        private CustomerBehaviour _currentCustomer;         // the customer that is being served
        private int _previousOffer;                         // the previous bid of the customer
        private int _latestOffer;                           // the latest bid of the customer
        private Items _offerItem;                           // the item that is up for pawing
        private bool _isGoblinOffering;                     // if the goblin is offering a item or requesting the item
        
        /// <summary>
        /// The customer offers the user an item for a price
        /// </summary>
        /// <param name="item">The item for sale</param>
        /// <param name="offerAmount">The initial amount the customer wants for it</param>
        /// <param name="customer">The new customer to serve</param>
        public void OfferUserItem(Items item,int offerAmount,CustomerBehaviour customer)
        {
            var itemManager = ItemManager.Instance;
            
            var value = UserData.Instance.netWorth < item.barValue.max
                ? UserData.Instance.netWorth
                : item.barValue.max;
            _latestOffer = 0;
            _previousOffer = offerAmount;
            _offerItem = item;
            _isGoblinOffering = true;
            _currentCustomer = customer;
            OnStartPawn?.Invoke(new MinMax<int>(_offerItem.barValue.min,value),offerAmount);
            
            itemManager.ItemEnableAndJump(_offerItem, itemManager.ItemCounterJumpLocation, itemManager.ItemCustomerJumpLocation);

        }
        /// <summary>
        /// The customer tries to buy an item from the user
        /// </summary>
        /// <param name="customer">The new customer to serve</param>
        /// <returns>The Item to buy</returns>
        public void RequestUserItem(CustomerBehaviour customer)
        {
            var itemManager = ItemManager.Instance;
            
            _offerItem = UserData.Instance.randomItem;
            _currentCustomer = customer;
            
            var offerOffset = customer.GetOfferOffset(_offerItem.value);
            var value = _currentCustomer.netWorth < _offerItem.barValue.max
                ? _currentCustomer.netWorth
                : _offerItem.barValue.max;
            _latestOffer = value;
            _previousOffer = _offerItem.value - offerOffset;
            _isGoblinOffering = false;
            Debug.LogWarning($"min max{(_offerItem.barValue.min,value)} value{_offerItem.value}");
            OnStartPawn?.Invoke(new MinMax<int>(_offerItem.barValue.min,value),_previousOffer);
            
            itemManager.ItemEnableAndJump(_offerItem, itemManager.ItemCounterJumpLocation, itemManager.ItemPlayerJumpLocation);
        }
        
        /// <summary>
        /// Processes the bid made by the user and determines the outcome
        /// </summary>
        /// <param name="bid">The bid amount placed by the user</param>
        public void CheckBid(int bid)
        {
            OnCheckBid?.Invoke(bid);

            if (IsBidAcceptable(bid))
            {
                Debug.Log("Bid accepted");
                AcceptBid(bid);
                return;
            }
            
            _currentCustomer.UpdateSatisfaction(false, 0.1f);
            var ple = IsBidOutOfRange(bid);
            if (ple)
            {
                Debug.LogError("Customer left the shop");
                LostInterest();
                return;
            }
            if (_isGoblinOffering ? bid < _latestOffer : bid > _latestOffer)
            {
                Debug.LogError("Customer left the shop");
                LostInterest();
                return;
            }
            _latestOffer = bid;
            Debug.LogError("Making new bid");
            MakeNewOffer(bid);
        }
        
        /// <summary>
        /// Checks if the customer accepts your price
        /// </summary>
        /// <param name="barValue">The users bid</param>
        /// <returns>If the customer accepts</returns>
        private bool IsBidAcceptable(int barValue) => 
            _currentCustomer.WillBuy(barValue,_previousOffer,_isGoblinOffering);
        
        /// <summary>
        /// Checks if the customer wants to leave the shop
        /// </summary>
        /// <param name="barValue">The users bid</param>
        /// <returns>If the customer wants to leave</returns>
        private bool IsBidOutOfRange(int barValue) =>
            !_currentCustomer.IsInterested(barValue,_previousOffer,_isGoblinOffering);
        
        /// <summary>
        /// Trades the item between the customer and the user
        /// Gives the money to the rightfully party
        /// Kicks the customer out
        /// Update customer satisfaction
        /// </summary>
        /// <param name="bid"></param>
        private void AcceptBid(int bid)
        {
            var itemManager = ItemManager.Instance;
            
            OnFinished?.Invoke(true, bid);
            DayLoopEvents.Instance.CustomerLeave?.Invoke();
            _currentCustomer.UpdateSatisfaction(true, 3);
            if (_isGoblinOffering)
            {
                UserData.Instance.BuyItem(_offerItem, bid, _currentCustomer);
                
                itemManager.ItemJumpAndDisable(_offerItem, itemManager.ItemPlayerJumpLocation);
                return;
            }
            UserData.Instance.SellItem(_offerItem, bid, _currentCustomer);
            
            itemManager.ItemJumpAndDisable(_offerItem, itemManager.ItemCustomerJumpLocation);
        }
        
        /// <summary>
        /// Makes the customer leave the shop
        /// </summary>
        private void LostInterest()
        {
            var itemManager = ItemManager.Instance;

            OnFinished?.Invoke(false, 0);
            DayLoopEvents.Instance.CustomerLeave?.Invoke();
            
            itemManager.ItemJumpAndDisable(_offerItem, itemManager.ItemPlayerJumpLocation);
        }
        
        /// <summary>
        /// Lets the customer make a new offer
        /// </summary>
        /// <param name="bid">The bid of the user</param>
        private void MakeNewOffer(int bid)
        {
            var newBid = _currentCustomer.MakeNewOffer(bid,_previousOffer);
            OnNewBidRound?.Invoke(newBid);
            NewBidRound(newBid);
        }
        
        /// <summary>
        /// Starts a new round of bidding
        /// </summary>
        /// <param name="newBid">The customers new bid</param>
        private void NewBidRound(int newBid)
        {
            _previousOffer = newBid;
            OnNewBidRound?.Invoke(newBid);
            OnCheckBid?.Invoke(newBid);
        }
    }
}
