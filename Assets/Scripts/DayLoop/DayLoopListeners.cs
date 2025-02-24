using UnityEngine;

namespace DayLoop
{
    public abstract class DayLoopListeners : MonoBehaviour
    {
        public void OnEnable()
        {
            DayLoopEvents.Instance.StartDay += StartDay;
            DayLoopEvents.Instance.EndDay += EndDay;
        }

        public void OnDisable()
        {
            DayLoopEvents.Instance.StartDay -= StartDay;
            DayLoopEvents.Instance.EndDay -= EndDay;
        }

        protected abstract void StartDay();
        protected abstract void EndDay();
    }
}
