using System;
using TMPro;
using UnityEngine;
using User;

namespace CurrencyDisplay
{
    public class CurrencyDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyText; // Display of current amount of currency
        /// <summary>
        /// subscribe to the OnCurrencyChanged Action from the UserData Singleton
        /// </summary>
        private void Awake()
        {
            UserData.Instance.OnCurrencyChanged += UpdateDisplay;
        }
        /// <summary>
        /// Update the display to the current currency
        /// </summary>
        /// <param name="currency">The new amount of currency</param>

        private void UpdateDisplay(int currency) => currencyText.text = $"{currency.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"))}$";
        
    }
}
