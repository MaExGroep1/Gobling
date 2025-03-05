using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DayLoop;
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

        protected override void CustomerLeave()
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
        }
        /// <summary>
        /// Stop waiting for the current customer to leave and get the next customer
        /// </summary>
        private void OnCustomerExitShop()
        {
            _lastCustomer.OnExitShop -= OnCustomerExitShop;
            ServeNewCustomer();
        }
    }
}
