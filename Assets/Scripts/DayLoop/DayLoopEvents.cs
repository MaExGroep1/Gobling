using System;
using UnityEngine;
using Util;

namespace DayLoop
{
    public class DayLoopEvents : Singleton<DayLoopEvents>
    {
        public Action StartDay; // action to invoke when the day starts
        public Action EndDay; // action to invoke when the day ends
        public Action CustomerLeave; // action to invoke when the customer leaves
        public Action GameOver; // action to invoke when the player has no money left
    }
}