using System;
using Newtonsoft.Json;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class TimeData
    {
        [JsonProperty]
        private int _currentDay;

        /// <summary>
        /// 1-based index.
        /// </summary>
        [JsonIgnore]
        public int CurrentDay => _currentDay;

        public void AddDay() =>
            _currentDay++;
    }
}