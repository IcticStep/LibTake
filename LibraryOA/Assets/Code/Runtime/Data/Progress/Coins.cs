using System;
using Newtonsoft.Json;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public class Coins
    {
        [JsonProperty]
        public int Count;
    }
}