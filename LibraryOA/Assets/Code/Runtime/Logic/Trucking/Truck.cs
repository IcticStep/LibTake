using System;
using Code.Runtime.Logic.Interactables;
using Code.Runtime.Services.Interactions.Truck;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Runtime.Logic.Trucking
{
    public sealed class Truck : Interactable
    {
        private UniTaskCompletionSource _completionSource = new();
        private ITruckInteractionService _truckInteractionService;
        public UniTask BooksTakenTask => _completionSource.Task;
        
        public event Action BooksTaken;

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
            
            BooksTaken?.Invoke();
            _completionSource.TrySetResult();
            _completionSource = new UniTaskCompletionSource();
        }
    }
}