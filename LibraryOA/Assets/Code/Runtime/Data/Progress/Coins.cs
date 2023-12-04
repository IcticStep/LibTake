using System;
using Newtonsoft.Json;
using UnityEngine.Serialization;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public class Coins
    {
        [JsonProperty]
        public int Amount;
    }
}