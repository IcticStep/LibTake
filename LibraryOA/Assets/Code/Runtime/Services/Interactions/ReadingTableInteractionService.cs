using System.Threading;
using Code.Runtime.Logic.Interactions.Data;
using Code.Runtime.Player;
using Code.Runtime.Services.Player;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Unity.VisualScripting;
using Progress = Code.Runtime.Logic.Progress;

namespace Code.Runtime.Services.Interactions
{
    [UsedImplicitly]
    internal sealed class ReadingTableInteractionService : IReadingTableInteractionService
    {
        private readonly IBookSlotInteractionService _bookSlotInteractionService;
        private readonly IPlayerProviderService _playerProviderService;
        private InteractablesScanner InteractablesScanner => _playerProviderService.InteractablesScanner;

        public ReadingTableInteractionService(IBookSlotInteractionService bookSlotInteractionService, IPlayerProviderService playerProviderService)
        {
            _bookSlotInteractionService = bookSlotInteractionService;
            _playerProviderService = playerProviderService;
        }

        public bool CanInteract(IBookStorage bookStorage) =>
            _bookSlotInteractionService.CanInteract(bookStorage);

        public void Interact(IBookStorage bookStorage, Progress progress)
        {
            _bookSlotInteractionService.Interact(bookStorage);
            progress.StartFilling();
        }
    }
}