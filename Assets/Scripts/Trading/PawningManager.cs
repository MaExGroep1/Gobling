using System;
using Customer;
using DayLoop;
using Item;
using Sound;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using User;
using Util;


namespace Trading
{
    public class PawningManager : Singleton<PawningManager>
    {
        public Items OfferItem { get; private set; }
        
        public Action<MinMax<int>, int, int, CustomerBehaviour, string> OnStartPawn;        // event triggered when a pawn transaction starts
        
        public Action<int> OnNewBidRound;                   // event when the customer gives a counteroffer
        public Action<int> OnCheckBid;                      // event triggered when a bid is made
        public Action<bool,int> OnFinished;            // event to trigger when the customer is done pawing
        
        private CustomerBehaviour _currentCustomer;         // the customer that is being served
        private int _previousOffer;                         // the previous bid of the customer
        private int _latestOffer;                           // the latest bid of the customer
        private bool _isGoblinOffering;                     // if the goblin is offering a item or requesting the item
        

        /// <summary>
        /// The customer offers the user an item for a price
        /// </summary>
        /// <param name="item">The item for sale</param>
        /// <param name="offerAmount">The initial amount the customer wants for it</param>
        /// <param name="customer">The new customer to serve</param>
        public void OfferUserItem(Items item,int offerAmount,CustomerBehaviour customer)
        {
            var maxValue = UserData.Instance.netWorth < item.barValue.max
                ? UserData.Instance.netWorth
                : item.barValue.max;
            var minValue = maxValue < item.barValue.min ? 0 : item.barValue.min;
            _latestOffer = 0;
            _previousOffer = offerAmount;
            OfferItem = item;
            _isGoblinOffering = true;
            _currentCustomer = customer;

            OnStartPawn?.Invoke(new MinMax<int>(minValue,maxValue), offerAmount, item.value, customer, "Buying");
        }
        /// <summary>
        /// The customer tries to buy an item from the user
        /// </summary>
        /// <param name="customer">The new customer to serve</param>
        /// <returns>The Item to buy</returns>
        public void RequestUserItem(CustomerBehaviour customer)
        {
            OfferItem = UserData.Instance.randomItem;
            _currentCustomer = customer;
            
            var offerOffset = customer.GetOfferOffset(OfferItem.value);
            var value = _currentCustomer.netWorth < OfferItem.barValue.max
                ? _currentCustomer.netWorth
                : OfferItem.barValue.max;
            _latestOffer = value;
            _previousOffer = OfferItem.value - offerOffset;
            _isGoblinOffering = false;
            
            OnStartPawn?.Invoke(new MinMax<int>(OfferItem.barValue.min,value), _previousOffer, OfferItem.value, customer, "Selling");
            
            ItemManager.Instance.ItemEnableAndJump(OfferItem, ItemManager.Instance.ItemCounterJumpLocation, ItemManager.Instance.ItemPlayerJumpLocation);
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
                LostInterest();
                return;
            }
            if (_isGoblinOffering ? bid < _latestOffer : bid > _latestOffer)
            {
                LostInterest();
                return;
            }
            _latestOffer = bid;
            MakeNewOffer(bid);
        }
        
        /// <summary>
        /// Kicks the customer out of the shop
        /// </summary>
        public void RejectOffer()
        {
            OnFinished?.Invoke(false, 0);
            DayLoopEvents.Instance.CustomerLeave?.Invoke(_isGoblinOffering);

            if (_isGoblinOffering) return;
            ItemManager.Instance.ItemJumpAndDisable(OfferItem, ItemManager.Instance.ItemPlayerJumpLocation);
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
            OnFinished?.Invoke(true, bid);
            DayLoopEvents.Instance.CustomerLeave?.Invoke(!_isGoblinOffering);

            _currentCustomer.UpdateSatisfaction(true, 3);
            if (_isGoblinOffering)
            {
                UserData.Instance.BuyItem(OfferItem, bid, _currentCustomer);
                ItemManager.Instance.ItemJumpAndDisable(OfferItem, ItemManager.Instance.ItemPlayerJumpLocation);
                return;
            }
            UserData.Instance.SellItem(OfferItem, bid, _currentCustomer);
        }
        
        /// <summary>
        /// Makes the customer leave the shop
        /// </summary>
        private void LostInterest()
        {

            OnFinished?.Invoke(false, 0);
            DayLoopEvents.Instance.CustomerLeave?.Invoke(_isGoblinOffering);

            if (_isGoblinOffering) return;
            ItemManager.Instance.ItemJumpAndDisable(OfferItem, ItemManager.Instance.ItemPlayerJumpLocation);
        }
        
        /// <summary>
        /// Lets the customer make a new offer
        /// </summary>
        /// <param name="bid">The bid of the user</param>
        private void MakeNewOffer(int bid)
        {
            SoundManager.Instance.OnCustomerGrunt();
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
