using System;
using TMPro;
using UnityEngine;
using User;

namespace CurrencyDisplay
{
    public class CurrencyDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currencyText;

        private void Awake()
        {
            UserData.Instance.OnCurrencyChanged += UpdateDisplay;
        }

        private void UpdateDisplay(int currency)
        {
            currencyText.text = $"{currency}$";
        }
    }
}
