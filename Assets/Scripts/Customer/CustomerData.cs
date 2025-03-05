using System.Linq;
using Enums;
using Item;
using UnityEngine;

namespace Customer
{
    [CreateAssetMenu(fileName = "Customer", menuName = "ScriptableObjects/CustomerData", order = 3)]
    public class CustomerData : ScriptableObject
    {
        public GameObject prefab; // the customers visuals
        public LootTable lootTable; // the lootTable to draw items from
        public float greediness; // high value will raise the goblins sell prices and lower buy prices
        public float speed; // the speed at which the customer moves
        public float turnSpeed; // the speed at which the customer turns
        public int netWorth; // the amount of currency the customer starts with
        public int income; // the amount of currency the customer earns daily
        public int startInventorySize; // the amount of items the customer starts with
        
        public float trustworthiness => lootTable.GetTrustworthiness(); // get trustworthiness form the lootTable
    }
}
