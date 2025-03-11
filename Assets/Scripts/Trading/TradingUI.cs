using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Util;

namespace Trading
{
    public class TradingUI : MonoBehaviour
    {
        [SerializeField] private GameObject uiParent;       // Parent UI element for trading
        [SerializeField] private TMP_Text bidAmount;        // The bid amount TextMeshPro Text
        [SerializeField] private Slider bidSlider;          // The bid amount slider UI element
        [SerializeField] private Button makeBidButton;      // Button to confirm bid
        
        /// <summary>
        /// Initializes event listeners and registers to the PawningManager's events.
        /// </summary>
        private void Awake()
        {
            PawningManager.Instance.OnStartPawn += OnStartPawn;
            PawningManager.Instance.OnFinished += OnBidFinish;
            PawningManager.Instance.OnNewBidRound += OnNewBid;
            
            bidSlider.onValueChanged.AddListener(OnBarChanged);
            makeBidButton.onClick.AddListener(OnBid);
        }
        
        /// <summary>
        /// Starts the pawn process by setting up the bid UI and displaying it.
        /// </summary>
        /// <param name="barValue">The min and max range for the bid slider</param>
        /// <param name="baseValue">The initial bid value</param>
        private void OnStartPawn(MinMax<int> barValue, int baseValue)
        {
            SetBidSlider(barValue, baseValue);
            uiParent.SetActive(true);
        }
        
        /// <summary>
        /// Updates the bid amount text when the slider value changes.
        /// </summary>
        /// <param name="barValue">The current slider value</param>
        private void OnBarChanged(float barValue) => bidAmount.text = Mathf.RoundToInt(bidSlider.value).ToString();
    
        /// <summary>
        /// Configures the bid slider's minimum, maximum, and initial value.
        /// </summary>
        /// <param name="barValue">The min and max range for the slider</param>
        /// <param name="itemValue">The default value to set on the slider</param>
        private void SetBidSlider(MinMax<int> barValue, int itemValue)
        {
            bidSlider.minValue = barValue.min;
            bidSlider.maxValue = barValue.max;
            bidSlider.value = itemValue;
        }

        /// <summary>
        /// Sends the final bid amount to the PawningManager for processing.
        /// </summary>
        private void OnBid() => PawningManager.Instance.CheckBid((int)bidSlider.value);

        /// <summary>
        /// Hides the trading UI after the bid process finishes.
        /// </summary>
        private void OnBidFinish(bool isPawnSuccessful, int finalOffer) => uiParent.SetActive(false);
        /// <summary>
        /// Sets the new bid on the bid slider
        /// </summary>
        /// <param name="bid"></param>
        
        private void OnNewBid(int bid) => bidSlider.value = bid;
    }
}
