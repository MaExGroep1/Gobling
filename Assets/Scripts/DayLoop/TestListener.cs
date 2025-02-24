using UnityEngine;

namespace DayLoop
{
    public class TestListener : DayLoopListeners
    {
        protected override void StartDay()
        {
            Debug.Log("StartDay");
        }

        protected override void EndDay()
        {
            Debug.Log("EndDay");
        }
    }
}
