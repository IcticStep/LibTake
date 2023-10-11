using Code.Runtime.Services.Player;
using Zenject;

namespace Code.Runtime.Services.Interactions
{
    internal sealed class BookTableInteractService
    {
        private readonly IPlayerProviderService _playerProviderService;

        public BookTableInteractService(IPlayerProviderService playerProviderService)
        {
            _playerProviderService = playerProviderService;
        }
    }
}