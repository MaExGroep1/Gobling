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
        public Action<MinMax<int>, int> OnStartPawn; // Event triggered when a pawn transaction starts
        public Action<int> OnNewBidRound;
        public Action<int> OnCheckBid; // Event triggered when a bid is made
        public Action<bool,int> OnFinished;
        
        private CustomerBehaviour _currentCustomer; // the customer that is being served
        private int _offerAmount;
        private int _originalBid;
        private int _lastOffer;
        private Items _offerItem;
        private bool _isGoblinOffering;
        private MinMax<int> _acceptableBidRange;
        
        /// <summary>
        /// The customer offers the player an item for a price
        /// </summary>
        /// <param name="item">The item for sale</param>
        /// <param name="offerAmount">The initial amount the customer wants for it</param>
        /// <param name="customer">The new customer to serve</param>
        public void OfferUserItem(Items item,int offerAmount,CustomerBehaviour customer)
        {
            var value = UserData.Instance.netWorth < item.barValue.max
                ? UserData.Instance.netWorth
                : item.barValue.max;
            _lastOffer = 0;
            _originalBid = offerAmount;
            _offerItem = item;
            _isGoblinOffering = true;
            _currentCustomer = customer;
            _offerAmount = offerAmount;
            OnStartPawn?.Invoke(new MinMax<int>(_offerItem.barValue.min,value),offerAmount);
        }
        /// <summary>
        /// The customer tries to buy an item from the player
        /// </summary>
        /// <param name="customer">The new customer to serve</param>
        /// <returns>The Item to buy</returns>
        public void RequestUserItem(CustomerBehaviour customer) {
            _offerItem = UserData.Instance.randomItem;
            _currentCustomer = customer;

            var offerOffset = customer.GetOfferOffset(_offerItem.value);
            var value = _currentCustomer.netWorth < _offerItem.barValue.max
                ? _currentCustomer.netWorth
                : _offerItem.barValue.max;
            _lastOffer = value;
            _originalBid = _offerItem.value - offerOffset;
            _isGoblinOffering = false;
            Debug.LogWarning($"min max{(_offerItem.barValue.min,value)} value{_offerItem.value}");
            OnStartPawn?.Invoke(new MinMax<int>(_offerItem.barValue.min,value),_originalBid);
        }
        
        /// <summary>
        /// Processes the bid made by the player and determines the outcome
        /// </summary>
        /// <param name="bid">The bid amount placed by the player</param>
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
            if (_isGoblinOffering ? bid < _lastOffer : bid > _lastOffer)
            {
                Debug.LogError("Customer left the shop");
                LostInterest();
                return;
            }
            _lastOffer = bid;
            Debug.LogError("Making new bid");
            MakeNewOffer(bid);
        }

        private bool IsBidAcceptable(int barValue)
        {
            return _currentCustomer.WillBuy(barValue,_originalBid,_isGoblinOffering);
        }

        private bool IsBidOutOfRange(int barValue)
        {
            return !_currentCustomer.IsInterested(barValue,_originalBid,_isGoblinOffering);
        }

        private void AcceptBid(int bid)
        {
            OnFinished?.Invoke(true, bid);
            DayLoopEvents.Instance.CustomerLeave?.Invoke();
            _currentCustomer.UpdateSatisfaction(true, 3);
            if (_isGoblinOffering)
            {
                UserData.Instance.BuyItem(_offerItem, bid, _currentCustomer);
                return;
            }
            UserData.Instance.SellItem(_offerItem, bid, _currentCustomer);
        }

        private void LostInterest()
        {
            OnFinished?.Invoke(false, 0);
            DayLoopEvents.Instance.CustomerLeave?.Invoke();
        }

        private void MakeNewOffer(int bid)
        {
            var newBid = _currentCustomer.MakeNewOffer(bid,_originalBid);
            OnNewBidRound?.Invoke(newBid);
            NewBidRound(newBid);
        }

        private void NewBidRound(int newBid)
        {
            _originalBid = newBid;
            OnNewBidRound?.Invoke(newBid);
            OnCheckBid?.Invoke(newBid);
        }
    }
}
