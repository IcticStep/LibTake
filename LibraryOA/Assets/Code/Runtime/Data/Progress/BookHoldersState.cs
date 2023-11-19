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
        private List<BookIdentifiedHolderData> _bookHolders = new();
        
        public BookData GetDataForId(string id) =>
            _bookHolders.FirstOrDefault(x => x.Id == id)?.BookData;
        
        public void SetDataForId(string id, BookData data)
        {
            BookIdentifiedHolderData savedData = _bookHolders.FirstOrDefault(x => x.Id == id);

            if(savedData is not null)
                _bookHolders.Remove(savedData);
            
            _bookHolders.Add(new BookIdentifiedHolderData(id, data));
        }
    }
}