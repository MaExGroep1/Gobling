using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Util;

public class Trading : MonoBehaviour
{
    [SerializeField] private Button confirmButton; // The Confirm bid button
    [SerializeField] private TMP_Text bidAmount; // The bid amount TextMeshPro Text

    private int _itemValue; // The Value of the item
    private MinMax<int> _itemValueRange; // The range of the value the player can buy/sell the item for

    
    /// <summary>
    /// Sets `_itemValue` and `_itemValueRange`.
    /// </summary>
    /// <param name="itemValueOverride">Override for `_itemValue`.</param>
    /// <param name="minMaxValueOverride">Override for `_itemValueRange`.</param>
    private void SetValueRange(int itemValueOverride, MinMax<int> minMaxValueOverride)
    {
        _itemValue = itemValueOverride;
        _itemValueRange = minMaxValueOverride;
    }
}
