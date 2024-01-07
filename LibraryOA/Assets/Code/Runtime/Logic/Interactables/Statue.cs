using Code.Runtime.Services.Interactions.Statue;
using Zenject;

namespace Code.Runtime.Logic.Interactables
{
    internal sealed class Statue : Interactable
    {
        private IStatueInteractionService _statueInteractionService;
        
        [Inject]
        private void Construct(IStatueInteractionService statueInteractionService) =>
            _statueInteractionService = statueInteractionService;

        public override bool CanInteract() =>
            _statueInteractionService.CanInteract();

        public override void Interact() =>
            _statueInteractionService.Interact();
    }
}