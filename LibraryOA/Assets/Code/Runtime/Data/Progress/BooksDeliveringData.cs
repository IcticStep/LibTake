using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public class BooksDeliveringData
    {
        [JsonProperty]
        private List<string> _preparedForDelivering = new();

        [JsonProperty]
        private List<string> _delivered = new();

        public IReadOnlyList<string> PreparedForDelivering => _preparedForDelivering;
        public IReadOnlyList<string> Delivered => _delivered;

        public void AddToPreparedForDelivering(string bookId) =>
            _preparedForDelivering.Add(bookId);
        
        public void AddToPreparedForDelivering(IEnumerable<string> bookId) =>
            _preparedForDelivering.AddRange(bookId);

        public void DeliverPrepared()
        {
            _delivered.AddRange(_preparedForDelivering);
            _preparedForDelivering.Clear();
        }
    }
}