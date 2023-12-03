using Code.Runtime.Logic.Interactions;
using Code.Runtime.Services.Interactions.Truck;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Runtime.Logic
{
    public sealed class Truck : Interactable
    {
        private UniTaskCompletionSource _completionSource = new();
        private ITruckInteractionService _truckInteractionService;
        public UniTask BooksTakenTask => _completionSource.Task;

        [Inject]
        private void Construct(ITruckInteractionService truckInteractionService) =>
            _truckInteractionService = truckInteractionService;

        public override bool CanInteract() =>
            _truckInteractionService.CanInteract();

        public override void Interact()
        {
            bool result = _truckInteractionService.TryInteract();

            if(!result)
                return;

            _completionSource.TrySetResult();
            _completionSource = new UniTaskCompletionSource();
        }
    }
}