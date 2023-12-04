using System;
using Newtonsoft.Json;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class TimeData
    {
        /// <summary>
        /// 1-based index.
        /// </summary>
        
        public int CurrentDay;
    }
}