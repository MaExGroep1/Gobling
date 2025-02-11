using UnityEngine;
using Enums;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemScriptableObject", order = 1)]
    public class ItemScriptableObject : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int value = 10;
        [SerializeField] private ItemType itemType = ItemType.Normal;
    }
}
