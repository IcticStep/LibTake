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
        private List<string> _currentInLibraryBooks = new();

        [JsonIgnore]
        public IReadOnlyList<string> PreparedForDelivering => _preparedForDelivering;
        [JsonIgnore]
        public IReadOnlyList<string> CurrentInLibraryBooks => _currentInLibraryBooks;

        public void AddToPreparedForDelivering(string bookId) =>
            _preparedForDelivering.Add(bookId);
        
        public void AddToPreparedForDelivering(IEnumerable<string> bookId) =>
            _preparedForDelivering.AddRange(bookId);

        public void RemoveFromLibrary(string book) =>
            _currentInLibraryBooks.Remove(book);

        public void DeliverPrepared()
        {
            _currentInLibraryBooks.AddRange(_preparedForDelivering);
            _preparedForDelivering.Clear();
        }
    }
}