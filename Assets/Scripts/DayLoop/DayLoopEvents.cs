using System;
using UnityEngine;
using Util;

namespace DayLoop
{
    public class DayLoopEvents : Singleton<DayLoopEvents>
    {
        public Action StartDay;
        public Action EndDay;
    }
}