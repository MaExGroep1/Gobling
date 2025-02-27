using Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class TradingUI : MonoBehaviour
{
    [SerializeField] private TMP_Text bidAmount; // The bid amount TextMeshPro Text
    [SerializeField] private Slider bidSlider; // The bid amount slider UI element
    
    [SerializeField] private ItemData itemData; // The item data reference

    
    /// <summary>
    /// Updates the bid amount text to match the current value of the bid slider.
    /// </summary>
    private void Update() => bidAmount.text = Mathf.Ceil(bidSlider.value).ToString();

    
    /// <summary>
    /// Sets the slider's min and max values based on the calculated item value percentage.
    /// </summary>
    /// <param name="item">The item being sold.</param>
    public void OnSellItem(Items item)
    {
        var barValue = item.CalculateValuePercent();
        bidSlider.minValue = barValue.min;
        bidSlider.maxValue = barValue.max;
    }    
}
