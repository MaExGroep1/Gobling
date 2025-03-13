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
            DayLoopEvents.Instance.CustomerLeave += CustomerLeave;
            DayLoopEvents.Instance.GameOver += GameOver;
        }

        /// <summary>
        /// Remove the start and end events to the StartDay() and EndDay()
        /// </summary>
        public void OnDisable()
        {
            DayLoopEvents.Instance.StartDay -= StartDay;
            DayLoopEvents.Instance.EndDay -= EndDay;
            DayLoopEvents.Instance.CustomerLeave -= CustomerLeave;
            DayLoopEvents.Instance.GameOver -= GameOver;
        }
        /// <summary>
        /// Event that gets called at the start of the day
        /// </summary>
        protected abstract void StartDay();
        /// <summary>
        /// Event that gets called at the end of the day
        /// </summary>
        protected abstract void EndDay();
        /// <summary>
        /// Event that gets called when a customer leaves the shop
        /// </summary>
        protected abstract void CustomerLeave(bool itemToGoblin);
        /// <summary>
        /// Event that gets called when the user has 0 currency
        /// </summary>
        protected abstract void GameOver();
    }
}
