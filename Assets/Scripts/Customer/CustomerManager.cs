using System;
using System.Collections.Generic;
using System.Linq;
using DayLoop;
using UnityEngine;

namespace Customer
{
    public class CustomerManager : DayLoopListeners
    {
        private List<CustomerBehaviour> _customerBehaviours;
        private CustomerBehaviour _customerTemplate;
        private Transform _customerSpawnPoint;

        private void Awake()
        {
            var AllCustomers= Resources.LoadAll<CustomerData>( "ScriptableObjects/Items");
            foreach (var customer in AllCustomers)
            {
                var newCustomer = Instantiate(_customerTemplate, _customerSpawnPoint);
                newCustomer.Initialize(customer);
                _customerBehaviours.Add(newCustomer);
            }
        }

        protected override void StartDay()
        {
            
        }

        protected override void EndDay()
        {
            throw new System.NotImplementedException();
        }
    }
}
