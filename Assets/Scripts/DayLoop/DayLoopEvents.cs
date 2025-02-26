using System;
using UnityEngine;
using Util;

namespace DayLoop
{
    public class DayLoopEvents : Singleton<DayLoopEvents>
    {
        public Action StartDay; // Action to invoke when the day starts
        public Action EndDay;   // Action to invoke when the day ends
    }
}