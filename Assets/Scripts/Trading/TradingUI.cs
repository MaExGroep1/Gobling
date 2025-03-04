using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Util;

namespace Trading
{
    public class TradingUI : MonoBehaviour
    {
        [SerializeField] private GameObject uiParent;
        
        [SerializeField] private TMP_Text bidAmount; // The bid amount TextMeshPro Text
        [SerializeField] private Slider bidSlider; // The bid amount slider UI element

        [SerializeField] private Button makeBidButton;

        private static int barValueMin;
        private static int barValueMax;



        private void Awake()
        {
            PawningManager.Instance.OnStartpawn += OnStartpawn;
            
            
            //!TODO make sure this removes UI elements after the item has been bought
        
            bidSlider.onValueChanged.AddListener(OnBarChanged);
            makeBidButton.onClick.AddListener(OnBid);
        }


        public void OnStartpawn(MinMax<int> barValue, int baseValue)
        {
            SetBidSlider(barValue, baseValue);
            uiParent.SetActive(true);
        }

        private void OnBarChanged(float barValue) => bidAmount.text = Mathf.Ceil(bidSlider.value).ToString();
    

        public void SetBidSlider(MinMax<int> barValue, int itemValue)
        {
            bidSlider.minValue = barValue.min;
            bidSlider.maxValue = barValue.max;
            bidSlider.value = itemValue;
        }

        private void OnBid() => PawningManager.Instance.CheckBid((int)bidSlider.value);

        public void OnBidFinish() => uiParent.SetActive(false);
    }
}
