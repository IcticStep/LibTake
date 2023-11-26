using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public class ProgressesStates
    {
        [JsonProperty]
        private Dictionary<string, float> _progressValues = new();

        public float GetDataForId(string id) =>
            _progressValues.TryGetValue(id, out float value)
                ? value
                : default(float);

        public void SetDataForId(string id, float value) =>
            _progressValues[id] = value;
    }
}