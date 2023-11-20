using Code.Runtime.Logic.Interactions;
using Code.Runtime.Services.Interactions.Truck;
using Zenject;

namespace Code.Runtime.Logic
{
    internal sealed class Truck : Interactable
    {
        private ITruckInteractionService _truckInteractionService;

        [Inject]
        private void Construct(ITruckInteractionService truckInteractionService) =>
            _truckInteractionService = truckInteractionService;

        public override bool CanInteract() =>
            _truckInteractionService.CanInteract();

        public override void Interact() => _truckInteractionService.Interact();
    }
}