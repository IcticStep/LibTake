using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public class BooksDeliveringData
    {
        [JsonProperty]
        private List<string> _currentInLibraryBooks = new();

        [JsonIgnore]
        public IReadOnlyList<string> CurrentInLibraryBooks => _currentInLibraryBooks;

        public void RemoveFromLibrary(string book) =>
            _currentInLibraryBooks.Remove(book);

        public void AddDeliveredBooks(IEnumerable<string> books) =>
            _currentInLibraryBooks.AddRange(books);
    }
}