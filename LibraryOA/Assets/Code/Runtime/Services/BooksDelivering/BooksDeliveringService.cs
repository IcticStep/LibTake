using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Random;
using JetBrains.Annotations;

namespace Code.Runtime.Services.BooksDelivering
{
    [UsedImplicitly]
    internal sealed class BooksDeliveringService : IBooksDeliveringService
    {
        private const int DeliverBooksAmount = 5;
        
        private readonly IPlayerProgressService _progressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IRandomService _randomService;

        private BooksDeliveringData DeliveringData => _progressService.Progress.WorldData.BooksDeliveringData;

        public BooksDeliveringService(IPlayerProgressService progressService, IStaticDataService staticDataService, IRandomService randomService)
        {
            _progressService = progressService;
            _staticDataService = staticDataService;
            _randomService = randomService;
        }

        public void DeliverBooks()
        {
            IReadOnlyList<string> booksToChoose = GetBooksToChoose();
            for(int i = 0; i < DeliverBooksAmount; i++)
            {
                int chosenIndex = _randomService.GetInRange(0, booksToChoose.Count);
                string chosenId = booksToChoose[chosenIndex];
                DeliveringData.AddToPreparedForDelivering(chosenId);
            }   
        }

        private IReadOnlyList<string> GetBooksToChoose() =>
            _staticDataService
                .AllBooks
                .Select(book => book.Id)
                .ToList();
    }
}