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
        public Action<int> OnBid; // Event triggered when a bid is made
        public Action<bool,int> OnFinished;
        
        private CustomerBehaviour _currentCustomer; // the customer that is being served
        private int _offerAmount;
        private int _originalBid;
        private bool _isOfferingItem;
        private MinMax<int> _acceptableBidRange;
        
        /// <summary>
        /// The customer offers the player an item for a price
        /// </summary>
        /// <param name="item">The item for sale</param>
        /// <param name="offerAmount">The initial amount the customer wants for it</param>
        /// <param name="customer">The new customer to serve</param>
        public void OfferUserItem(Items item,int offerAmount,CustomerBehaviour customer)
        {
            _isOfferingItem = true;
            _currentCustomer = customer;
            _offerAmount = offerAmount;
            OnStartPawn?.Invoke(item.barValue,offerAmount);
        }
        /// <summary>
        /// The customer tries to buy an item from the player
        /// </summary>
        /// <param name="customer">The new customer to serve</param>
        /// <returns>The Item to buy</returns>
        public void RequestUserItem(CustomerBehaviour customer)
        {
            _isOfferingItem = false;
            var item = UserData.Instance.randomItem;
            var offerOffset = customer.GetOfferOffset(item.value);
            _currentCustomer = customer;
            OnStartPawn?.Invoke(item.barValue,item.value + offerOffset);
        }
        
        /// <summary>
        /// Processes the bid made by the player and determines the outcome
        /// </summary>
        /// <param name="bid">The bid amount placed by the player</param>
        public void CheckBid(int bid)
        {
            OnBid?.Invoke(bid);

            if (IsBidAcceptable(bid))
            {
                AcceptBid(bid);
                return;
            }
            
            _currentCustomer.UpdateSatisfaction(false, 0.5f);
            
            if (IsBidOutOfRange(bid))
            {
                LostInterest();
                return;
            }
            
            MakeNewOffer(bid);
        }

        private bool IsBidAcceptable(int barValue)
        {
            return _currentCustomer.WillBuy(barValue,_originalBid,_isOfferingItem);
        }

        private bool IsBidOutOfRange(int barValue)
        {
            return _currentCustomer.IsInterested(barValue,_originalBid,_isOfferingItem);
        }

        private void AcceptBid(int bid)
        {
            OnFinished?.Invoke(true, bid);
            UserData.Instance.ChangeNetWorth(_isOfferingItem ? bid : -bid);
            DayLoopEvents.Instance.CustomerLeave?.Invoke();
            _currentCustomer.UpdateSatisfaction(true, 2);
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
            
        }
    }
}
