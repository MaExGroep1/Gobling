using System;
using DayLoop;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

namespace Difficulty
{
    public class DifficultyUI : MonoBehaviour
    {
        [SerializeField] private GameObject difficultyUI;   // the parent of the main menu
        [SerializeField] private Button normal;             // the normal difficulty button
        [SerializeField] private Button easy;               // the easy difficulty button

        /// <summary>
        /// Starts listening to the buttons
        /// </summary>
        private void Awake()
        {
            easy.onClick.AddListener(StartEasy);
            normal.onClick.AddListener(StartNormal);
        }
        
        /// <summary>
        /// Deactivates the UI and starts the game
        /// </summary>
        private void StartNormal()
        {
            difficultyUI.SetActive(false);
            DayLoopEvents.Instance.StartDay?.Invoke();
            DifficultyManager.Instance.SetDifficultyNormal();
        }

        /// <summary>
        /// Deactivates the UI and starts the game
        /// </summary>
        private void StartEasy()
        {
            difficultyUI.SetActive(false);
            DayLoopEvents.Instance.StartDay?.Invoke();
            DifficultyManager.Instance.SetDifficultyEasy();
        }
    }
}
