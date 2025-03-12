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
        public Action OnExitShop;                                       // the action that gets called on a customer exiting the shop
        
        public Action OnAtCounter;                                      //  the action that gets called on a customer arriving to the counter

        [SerializeField] private CustomerBehaviour customerTemplate;    // template to instantiate when spawning in the customers
        [SerializeField] private Transform customerSpawnPoint;          // the spawn point of the customers
        [SerializeField] private Transform[] counterPath, exitPath;     // the spawn point of the customers

        private readonly List<CustomerBehaviour> _customers = new();    // list of all customers
        private CustomerBehaviour _lastCustomer;                        // last customer to visit the shop
        
        /// <summary>
        /// Draws the path of the customers
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
            for(var i = 1; i < counterPath.Length; i++)
             Gizmos.DrawLine(counterPath[i].position, counterPath[i - 1].position);
            
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
            for(var i = 1; i < exitPath.Length; i++)
                Gizmos.DrawLine(exitPath[i].position, exitPath[i - 1].position);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(exitPath[0].position, 0.1f);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(counterPath[0].position, 0.1f);
        }

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
            _lastCustomer.ExitShop(exitPath,ServeNewCustomer);
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
        /// Kicks the current customer out
        /// </summary>
        protected override void CustomerLeave(bool itemToGoblin)
        {
            if (itemToGoblin)
            {
                StartCoroutine(WaitForCustomerToTakeItem());
                return;
            }
            RemoveCustomer();
        }

        /// <summary>
        /// Deletes all the customers
        /// </summary>

        protected override void GameOver()
        {
            foreach (var customer in _customers.ToList())
            {
                _customers.Remove(customer);
                Destroy(customer.gameObject);
            }
        }

        /// <summary>
        /// Select a random customer to serve that isn't the last customer and waits for the next customer to leave
        /// </summary>
        private void ServeNewCustomer()
        {
            if(_lastCustomer) _lastCustomer.gameObject.SetActive(false);
            var validCustomers = new List<CustomerBehaviour>();
            validCustomers.AddRange(_customers);
            validCustomers.Remove(_lastCustomer);
            var customer = validCustomers[Random.Range(0, validCustomers.Count)];
            customer.EnterShop(counterPath,OnAtCounter);
            _lastCustomer = customer;
        }

        private IEnumerator WaitForCustomerToTakeItem()
        {
            yield return StartCoroutine(_lastCustomer.TakeItem());
            RemoveCustomer();
        }
        
    }
}
