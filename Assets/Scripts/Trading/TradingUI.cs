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
        [SerializeField] private GameObject uiParent;       // parent UI element for trading
        [SerializeField] private TMP_Text bidAmount;        // the bid amount TextMeshPro Text
        [SerializeField] private Slider bidSlider;          // the bid amount slider UI element
        [SerializeField] private Button makeBidButton;      // button to confirm bid
        [SerializeField] private Button rejectButton;      // button to confirm bid
        
        [SerializeField] private Transform bottomPosition;        // the position the UI needs to start in or idle in
        [SerializeField] private Transform topPosition;       // the position the button needs to move to
        
        [SerializeField] private float scaleTime;
        
        /// <summary>
        /// Initializes event listeners and registers to the PawningManager's events
        /// </summary>
        private void Awake()
        {
            PawningManager.Instance.OnStartPawn += OnStartPawn;
            PawningManager.Instance.OnFinished += OnBidFinish;
            PawningManager.Instance.OnNewBidRound += OnNewBid;
            
            bidSlider.onValueChanged.AddListener(OnBarChanged);
            makeBidButton.onClick.AddListener(OnBid);
            rejectButton.onClick.AddListener(OnReject);
        }
        

        /// <summary>
        /// Starts the pawn process by setting up the bid UI and displaying it
        /// </summary>
        /// <param name="barValue">The min and max range for the bid slider</param>
        /// <param name="baseValue">The initial bid value</param>
        private void OnStartPawn(MinMax<int> barValue, int baseValue)
        {
            SetBidSlider(barValue, baseValue);
            MoveIn();
        }
        
        /// <summary>
        /// Updates the bid amount text when the slider value changes
        /// </summary>
        /// <param name="barValue">The current slider value</param>
        private void OnBarChanged(float barValue) => bidAmount.text = Mathf.RoundToInt(bidSlider.value).ToString();
    
        /// <summary>
        /// Configures the bid slider's minimum, maximum, and initial value
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
        /// Sends the final bid amount to the PawningManager for processing
        /// </summary>
        private void OnBid() => PawningManager.Instance.CheckBid((int)bidSlider.value);

        /// <summary>
        /// Hides the trading UI after the bid process finishes
        /// </summary>
        private void OnBidFinish(bool isSuccess, int amount)
        {
            MoveAway();
        }

        /// <summary>
        /// Sets the new bid on the bid slider
        /// </summary>
        /// <param name="bid"></param>
        private void OnNewBid(int bid) => bidSlider.value = bid;
        
        /// <summary>
        /// Moves UI in to frame when starting the bid
        /// </summary>
        private void MoveIn()
        {
            LeanTween.scale(uiParent, Vector3.one, 1).setEaseOutBack();
        }
        
        /// <summary>
        /// Moves UI away after it's done being used by the player
        /// </summary>
        private void MoveAway()
        {
            LeanTween.scale(uiParent, Vector3.zero, 1).setEaseInBack();
        }
        
        /// <summary>
        /// Kicks the customer out
        /// </summary>
        private static void OnReject()
        {
            PawningManager.Instance.RejectOffer();
        }
    }
}
