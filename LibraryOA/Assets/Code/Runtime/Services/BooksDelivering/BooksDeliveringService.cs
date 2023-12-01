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
        private readonly IPlayerProgressService _progressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IRandomService _randomService;

        private BooksDeliveringData DeliveringData => _progressService.Progress.WorldData.BooksDeliveringData;
        private int DeliverBooksAmount => _staticDataService.BooksDelivering.BooksPerDeliveringAmount;

        public BooksDeliveringService(IPlayerProgressService progressService, IStaticDataService staticDataService, IRandomService randomService)
        {
            _progressService = progressService;
            _staticDataService = staticDataService;
            _randomService = randomService;
        }

        public void DeliverBooksInTruck()
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