using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Interactions.Truck;
using Code.Runtime.Services.Random;
using JetBrains.Annotations;

namespace Code.Runtime.Services.BooksDelivering
{
    [UsedImplicitly]
    internal sealed class BooksDeliveringService : IBooksDeliveringService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IRandomService _randomService;
        private readonly ITruckInteractionService _truckInteractionService;

        private int CurrentDayBooksDelivering => _staticDataService.BookReceiving.BooksPerDeliveringAmount;

        public BooksDeliveringService(IStaticDataService staticDataService,
            IRandomService randomService, ITruckInteractionService truckInteractionService)
        {
            _staticDataService = staticDataService;
            _randomService = randomService;
            _truckInteractionService = truckInteractionService;
        }

        public void DeliverBooksInTruck()
        {
            IReadOnlyList<string> booksToChoose = GetBooksToChoose();
            for(int i = 0; i < CurrentDayBooksDelivering; i++)
            {
                int chosenIndex = _randomService.GetInRange(0, booksToChoose.Count);
                string chosenId = booksToChoose[chosenIndex];
                _truckInteractionService.PutBookInTruck(chosenId);
            }   
        }

        private IReadOnlyList<string> GetBooksToChoose() =>
            _staticDataService
                .AllBooks
                .Select(book => book.Id)
                .ToList();
    }
}