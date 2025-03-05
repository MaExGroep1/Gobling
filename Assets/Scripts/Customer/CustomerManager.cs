using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DayLoop;
using Item;
using UnityEngine;
using UnityEngine.Serialization;
using Util;
using Random = UnityEngine.Random;

namespace Customer
{
    public class CustomerManager : DayLoopListeners
    {
        private readonly List<CustomerBehaviour> _customers = new(); // List of all customers
        private CustomerBehaviour _lastCustomer; // Last customer to visit the shop

        [SerializeField] private CustomerBehaviour customerTemplate; // Template to instantiate when spawning in the customers
        [SerializeField] private Transform customerSpawnPoint,customerEntryPoint, customerTradePoint, customerExitPoint; // The poi's of the customers
        [SerializeField] private float speed; // Speed the customers travel
        /// <summary>
        /// Get all customers and 
        /// </summary>
        private void Awake()
        {
            SaveAllCustomers();
            StartCoroutine(TemporaryWait());
        }
        
        /// <summary>
        /// TODO: Remove this function to animation
        /// </summary>
        /// <returns></returns>
        private IEnumerator TemporaryWait()
        {
            yield return new WaitForSeconds(1);
            DayLoopEvents.Instance.StartDay?.Invoke();
        }
        /// <summary>
        /// Makes the current customer leave the store
        /// </summary>
        private void RemoveCustomer()
        {
            _lastCustomer.ExitShop(customerExitPoint, speed);
        }
        /// <summary>
        /// Instantiates all customers and saves them on a list
        /// </summary>
        private void SaveAllCustomers()
        {
            var allCustomers = Resources.LoadAll<CustomerData>( "ScriptableObjects/Customers");
            foreach (var customer in allCustomers)
            {
                var newCustomer = Instantiate(customerTemplate, transform);
                newCustomer.Initialize(customer);
                _customers.Add(newCustomer);
                newCustomer.gameObject.SetActive(false);
            }
        }
        /// <summary>
        /// Select a random customer to serve
        /// </summary>
        protected override void StartDay()
        {
            ServeNewCustomer();
        }
        
        /// <summary>
        /// Kicks the current customer out
        /// </summary>
        protected override void EndDay()
        {
            RemoveCustomer();
        }
        /// <summary>
        /// Select a random customer to serve that isn't the last customer and waits for the next customer to leave
        /// </summary>
        private void ServeNewCustomer()
        {
            var validCustomers = new List<CustomerBehaviour>();
            validCustomers.AddRange(_customers);
            validCustomers.Remove(_lastCustomer);
            var customer = validCustomers[Random.Range(0, validCustomers.Count)];
            customer.EnterShop(customerEntryPoint,customerTradePoint,speed);
            _lastCustomer = customer;
            customer.OnExitShop += OnCustomerExitShop;
            customer.OnReachCounter += StartMoveToCounter;
        }
        /// <summary>
        /// Stop waiting for the current customer to leave and get the next customer
        /// </summary>
        private void OnCustomerExitShop()
        {
            _lastCustomer.OnExitShop -= OnCustomerExitShop;
            _lastCustomer.OnReachCounter -= StartMoveToCounter;

            ServeNewCustomer();
        }
        
        
        //sprint review bullshit
        
        public List<Items> Items = new List<Items>();
        [SerializeField] private Items item;
        [SerializeField] private GameObject itemSpawnPoint;
        [SerializeField] private GameObject itemCounterPositionPoint;
        [SerializeField] private float jumpWaitTime;
        [SerializeField] private float jumpLeaveWaitTime;
        [SerializeField] private GameObject buyButton;
        
        private Items currentItem;

        public void BuyItem()
        {
            StartMoveAwayFromCounter();
            RemoveCustomer();
        }

        public void RejectItem()
        {
            StartCoroutine(MoveAwayFromCounter());
            RemoveCustomer();
        }
        
        private void GetNewItem()
        {
            if(currentItem) Destroy(currentItem.gameObject);
            item = Items[Random.Range(0, Items.Count)];
            Items spawnedItem = Instantiate(item, itemSpawnPoint.transform.position, Quaternion.identity);
            currentItem = spawnedItem;
        }

        private void StartMoveToCounter()
        {
            currentItem.Activate();
            StartCoroutine(MoveToCounter());
        }

        private IEnumerator MoveToCounter()
        {
            yield return new WaitForSeconds(jumpWaitTime);
            currentItem.JumpToPosition(itemCounterPositionPoint.transform.position);
            buyButton.SetActive(true);
        }
        
        private void StartMoveAwayFromCounter()
        {
            currentItem.JumpToPosition(itemSpawnPoint.transform.position);
            StartCoroutine(MoveAwayFromCounter());
        }

        private IEnumerator MoveAwayFromCounter()
        {
            yield return new WaitForSeconds(jumpLeaveWaitTime);
            currentItem.Deactivate();
            buyButton.SetActive(false);
            yield return new WaitForSeconds(3);
            GetNewItem();
        }

        private void Start()
        {
            GetNewItem();
        }
    }
}
