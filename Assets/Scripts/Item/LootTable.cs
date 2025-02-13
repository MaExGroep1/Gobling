using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "LootTable", menuName = "ScriptableObjects/LootTable", order = 2)]
    public class LootTable : ScriptableObject
    {
        public ItemData[] items;
    }
}
