using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public class CraftingTableStates
    {
        [JsonProperty]
        private Dictionary<string, Type> _stateTypes = new();

        public Type GetDataForId(string id) =>
            _stateTypes.GetValueOrDefault(id);

        public void SetDataForId(string id, Type value) =>
            _stateTypes[id] = value;
    }
}