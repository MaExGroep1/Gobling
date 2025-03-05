using System;
using TMPro;
using UnityEngine;
using User;

namespace CurrencyDisplay
{
    public class CurrencyDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] currencyText = new TextMeshProUGUI[6]; // Display of current amount of currency
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

        private void UpdateDisplay(int currency)
        {
            var text = currency.ToString();
            for (var i = 0; i < 6; i++)
            {
                var j = i + 1;
                var display = text.Length > i ? text[^j].ToString(): "0";
                currencyText[i].text = display;
            }
        }
        
    }
}
