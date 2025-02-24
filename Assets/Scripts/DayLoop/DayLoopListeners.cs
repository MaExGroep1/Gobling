using UnityEngine;

namespace DayLoop
{
    public abstract class DayLoopListeners : MonoBehaviour
    {
        /// <summary>
        /// Add the start and end events to the StartDay() and EndDay()
        /// </summary>
        public void OnEnable()
        {
            DayLoopEvents.Instance.StartDay += StartDay;
            DayLoopEvents.Instance.EndDay += EndDay;
        }
        /// <summary>
        /// Remove the start and end events to the StartDay() and EndDay()
        /// </summary>
        public void OnDisable()
        {
            DayLoopEvents.Instance.StartDay -= StartDay;
            DayLoopEvents.Instance.EndDay -= EndDay;
        }
        /// <summary>
        /// Event that gets called at the start of the day
        /// </summary>
        protected abstract void StartDay();
        /// <summary>
        /// Event that gets called at the end of the day
        /// </summary>
        protected abstract void EndDay();
    }
}
