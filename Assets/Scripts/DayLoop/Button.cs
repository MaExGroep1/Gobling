using UnityEngine;

namespace DayLoop
{
    public class Button : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button startButton;
        [SerializeField] private UnityEngine.UI.Button endButton;

        private void Awake()
        {
            startButton.onClick.AddListener(StartDay);
            endButton.onClick.AddListener(EndDay);
        }

        private static void EndDay() => DayLoopEvents.Instance.EndDay?.Invoke();

        private static void StartDay() => DayLoopEvents.Instance.StartDay?.Invoke();
    }
    
}
