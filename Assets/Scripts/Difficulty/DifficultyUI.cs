using System;
using DayLoop;
using UnityEngine;
using UnityEngine.SceneManagement;
using Button = UnityEngine.UI.Button;

namespace Difficulty
{
    public class DifficultyUI : MonoBehaviour
    {
        [SerializeField] private GameObject difficultyUI;
        [SerializeField] private Button normal;
        [SerializeField] private Button easy;

        private void Awake()
        {
            easy.onClick.AddListener(StartEasy);
            normal.onClick.AddListener(StartNormal);
        }

        private void StartNormal()
        {
            difficultyUI.SetActive(false);
            DayLoopEvents.Instance.StartDay?.Invoke();
            DifficultyManager.Instance.SetDifficultyNormal();
        }

        private void StartEasy()
        {
            difficultyUI.SetActive(false);
            DayLoopEvents.Instance.StartDay?.Invoke();
            DifficultyManager.Instance.SetDifficultyEasy();
        }
    }
}
