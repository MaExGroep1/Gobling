using System;
using System.Globalization;
using Customer;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using User;
using Util;

namespace Trading
{
    public class TradingUI : MonoBehaviour
    {
        [Header("Default")]
        [SerializeField] private GameObject uiParent;               // parent UI element for trading
        [SerializeField] private TMP_Text bidAmount;                // the bid amount TextMeshPro Text
        [SerializeField] private Slider bidSlider;                  // the bid amount slider UI element
        [SerializeField] private Button makeBidButton;              // button to confirm bid
        [SerializeField] private Button rejectButton;               // button to reject bid
        
        [Header("Positions")]
        [SerializeField] private Transform bottomPosition;          // the position the UI needs to start in or idle in
        [SerializeField] private Transform topPosition;             // the position the button needs to move to
        [SerializeField] private Transform overTopPosition;         // the position the button needs to move to
        
        [Header("Easy mode text")]
        [SerializeField] private TMP_Text satisfaction;             // the easy satisfaction meter
        [SerializeField] private TMP_Text baseValue;                // the easy base value
        
        [Header("Base value buy")]
        [SerializeField] private Button baseValueBuyButton;         // button to buy the base value
        [SerializeField] private GameObject baseValueBuyObject;     // the text area where baseValueBuy text is displayed
        [SerializeField] private TMP_Text baseValueBuy;             // the paid base value text
        [SerializeField] private TMP_Text baseValueBuyButtonText;   // the text on the baseValueBuy button
        [SerializeField] private int baseValueBuyAmount;            // the amount of currency it costs to see the base value
        
        [Header("Buying or selling")]
        [SerializeField] private GameObject buyingOrSellingParent;
        [SerializeField] private TMP_Text buyingOrSellingText;

        
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
            baseValueBuyButton.onClick.AddListener(ShowValue);
        }

        /// <summary>
        /// Starts the pawn process by setting up the bid UI and displaying it
        /// </summary>
        /// <param name="barValues">The min and max range for the bid slider</param>
        /// <param name="barValue">The initial bid value</param>
        /// <param name="itemBaseValue">The base value of the item</param>
        /// <param name="customer">The current customer</param>
        /// <param name="topText">Text to show at the top of the screen</param>
        private void OnStartPawn(MinMax<int> barValues, int barValue, int itemBaseValue, CustomerBehaviour customer, string topText)
        {
            baseValueBuyObject.SetActive(false);
            baseValueBuyButton.interactable = true;
            baseValueBuyButtonText.text = $"Get base value: {baseValueBuyAmount}$";
            buyingOrSellingText.text = topText;
                
            baseValue.text = $"Base value: {itemBaseValue}";
            baseValueBuy.text = $"Base value: {itemBaseValue}";
            satisfaction.text = $"Satisfaction: {Math.Round(customer.satisfaction * 100,2)}%";
            SetBidSlider(barValues, barValue);
            uiParent.SetActive(true);
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
        private void OnBid() => PawningManager.Instance.CheckBid(Mathf.RoundToInt(bidSlider.value));

        /// <summary>
        /// Hides the trading UI after the bid process finishes
        /// </summary>
        private void OnBidFinish(bool isSuccess, int amount)
        {
            uiParent.SetActive(false);
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
            LeanTween.move(buyingOrSellingParent, topPosition.transform.position, 3).setEase(LeanTweenType.easeOutBack);
            LeanTween.move(uiParent, topPosition.transform.position, 3).setEase(LeanTweenType.easeOutBack);
        }
        
        /// <summary>
        /// Moves UI away after it's done being used by the player
        /// </summary>
        private void MoveAway()
        {
            LeanTween.move(buyingOrSellingParent, overTopPosition.transform.position, 3).setEase(LeanTweenType.easeInBack);
            LeanTween.move(uiParent, bottomPosition.transform.position, 3).setEase(LeanTweenType.easeInBack);
        }

        /// <summary>
        /// Kicks the customer out
        /// </summary>
        private static void OnReject()
        {
            PawningManager.Instance.RejectOffer();
        }

        /// <summary>
        /// Shows the base value
        /// </summary>
        private void ShowValue()
        {
            baseValueBuyObject.SetActive(true);
            baseValueBuyButton.interactable = false;
            UserData.Instance.ChangeNetWorth(-baseValueBuyAmount);
        }
    }
}
