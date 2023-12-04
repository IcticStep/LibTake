using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Services.Random;
using JetBrains.Annotations;

namespace Code.Runtime.Services.BooksReceiving
{
    [UsedImplicitly]
    internal sealed class BooksReceivingService : IBooksReceivingService
    {
        private readonly IPersistantProgressService _progressService;
        private readonly IRandomService _randomService;

        private BooksDeliveringData BooksDeliveringData => _progressService.Progress.WorldData.BooksDeliveringData;
        private IReadOnlyList<string> CurrentInLibraryBooks => BooksDeliveringData.CurrentInLibraryBooks;

        public bool LibraryHasBooks => CurrentInLibraryBooks.Any();
        public int BooksInLibrary => CurrentInLibraryBooks.Count;

        public BooksReceivingService(IPersistantProgressService progressService, IRandomService randomService)
        {
            _progressService = progressService;
            _randomService = randomService;
        }

        public string SelectBookForReceiving()
        {
            int index = _randomService.GetInRange(0, CurrentInLibraryBooks.Count);
            return CurrentInLibraryBooks[index];
        }

        public void ReceiveBook(string book) =>
            BooksDeliveringData.RemoveFromLibrary(book);
    }
}