using System.Linq;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Item
{
    [CreateAssetMenu(fileName = "LootTable", menuName = "ScriptableObjects/LootTable", order = 2)]
    public class LootTable : ScriptableObject
    {
        [SerializeField] private ItemData[] items; // all items in the loot table
        
        /// <summary>
        /// Get a random piece of loot from the loot table
        /// </summary>
        /// <returns>Random piece of loot</returns>
        public ItemData GetRandomLoot() => items[Random.Range(0, items.Length)];
        
        /// <summary>
        /// Gets the percentage of not sabotaged items 
        /// </summary>
        /// <returns>Percentage of not sabotaged items</returns>
        public float GetTrustworthiness()
        {
            var itemCount = items.Length;
            var badItemCount = items.Count(item => item.itemType == ItemType.Sabotaged);
            
            // ReSharper disable once PossibleLossOfFraction
            return 1 - badItemCount / itemCount;
        }

    }
}
