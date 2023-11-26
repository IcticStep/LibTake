using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public class BookHoldersState
    {
        [JsonProperty]
        private Dictionary<string, BookData> _bookHolders = new();
        
        public BookData GetDataForId(string id) =>
            _bookHolders.TryGetValue(id, out BookData data)
                ? data
                : default(BookData);
        
        public void SetDataForId(string id, BookData data) =>
            _bookHolders[id] = data;
    }
}