using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Customer
{
    public class RoamingManager : MonoBehaviour
    {
        [SerializeField] private List<CustomerRoam> customersInShop = new(); // List of all customers
        [SerializeField] private CustomerManager customerManager; // The manager of the customers
        [SerializeField] private Transform entrancePoint;

        private void Awake()
        {
            customerManager.OnCustomerEntersShop += CustomerInShop;
            customerManager.OnCustomerExitsShop += CustomerOutShopp;
        }

        private void CustomerOutShopp(CustomerBehaviour customer)
        {
            var roam = customer.CustomerRoam;
            customersInShop.Remove(roam);
            roam.SetTarget(entrancePoint);
            roam.GetToDestination();
        }

        private void CustomerInShop(CustomerBehaviour customer)
        {
            customersInShop.Add(customer.CustomerRoam);
        }
    }
}
