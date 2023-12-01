using System;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class TimeData
    {
        private int _currentDay;
        
        /// <summary>
        /// 1-based index.
        /// </summary>
        public int CurrentDay => _currentDay;

        public void AddDay() =>
            _currentDay++;
    }
}