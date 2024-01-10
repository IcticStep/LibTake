using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Data;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Books.Receiving;
using Code.Runtime.Services.Days;
using Code.Runtime.Services.Interactions.Truck;
using Code.Runtime.Services.Random;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.Books.Delivering
{
    [UsedImplicitly]
    internal sealed class BooksDeliveringService : IBooksDeliveringService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IRandomService _randomService;
        private readonly ITruckInteractionService _truckInteractionService;
        private readonly IDaysService _daysService;
        private readonly IBooksReceivingService _booksReceivingService;

        public BooksDeliveringService(IStaticDataService staticDataService, IRandomService randomService, ITruckInteractionService truckInteractionService,
            IDaysService daysService, IBooksReceivingService booksReceivingService)
        {
            _staticDataService = staticDataService;
            _randomService = randomService;
            _truckInteractionService = truckInteractionService;
            _daysService = daysService;
            _booksReceivingService = booksReceivingService;
        }

        public void DeliverBooksInTruck()
        {
            IReadOnlyList<string> booksToChoose = GetBooksToChoose();
            int booksToDeliver = GetCurrentDayBooksDelivering();
            for(int i = 0; i < booksToDeliver; i++)
            {
                int chosenIndex = _randomService.GetInRange(0, booksToChoose.Count);
                string chosenId = booksToChoose[chosenIndex];
                _truckInteractionService.PutBookInTruck(chosenId);
            }   
        }

        private int GetCurrentDayBooksDelivering()
        {
            int day = _daysService.CurrentDay;
            int inLibrary = _booksReceivingService.BooksInLibrary;
            
            int shouldBeInLibrary = _staticDataService.BookDelivering.GetBooksShouldBeInLibraryForDay(day);
            int toDeliver = Mathf.Max(shouldBeInLibrary - inLibrary, 1);
            Debug.Log($"Books to deliver: {toDeliver}.");
            return toDeliver;
        }

        private IReadOnlyList<string> GetBooksToChoose() =>
            _staticDataService
                .AllBooks
                .Select(book => book.Id)
                .ToList();
    }
}