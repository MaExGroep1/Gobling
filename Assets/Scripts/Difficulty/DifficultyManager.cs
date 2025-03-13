using UnityEngine;
using Util;

namespace Difficulty
{
    public class DifficultyManager : Singleton<DifficultyManager>
    {
        [SerializeField] private GameObject 
            satisfaction,               // the customer satisfaction display
            baseValue,                  // the item base value display
            baseValueBuyButton;         // the paid for base value button

        /// <summary>
        /// Sets the difficulty to easy
        /// </summary>
        public void SetDifficultyEasy()
        {
            satisfaction.SetActive(true);
            baseValue.SetActive(true);
            baseValueBuyButton.SetActive(false);
        }
        
        /// <summary>
        /// Sets the difficulty to normal
        /// </summary>

        public void SetDifficultyNormal()
        {
            satisfaction.SetActive(false);
            baseValue.SetActive(false);
            baseValueBuyButton.SetActive(true);
        }
    }
}
