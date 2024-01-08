using System;
using Code.Runtime.Services.Interactions.Statue;
using Code.Runtime.Services.Interactions.Statue.Result;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Statue
{
    internal sealed class Statue : Interactable
    {
        private IStatueInteractionService _statueInteractionService;

        public event Action<int> MoneySpent;
        public event Action<int> LivesRestored;
        
        [Inject]
        private void Construct(IStatueInteractionService statueInteractionService) =>
            _statueInteractionService = statueInteractionService;

        public override bool CanInteract() =>
            _statueInteractionService.CanInteract();

        public override void Interact()
        {
            if(!CanInteract())
                return;
            
            Result result = _statueInteractionService.Interact();
            NotifyAboutResult(result);
        }

        private void NotifyAboutResult(Result result)
        {
            if(result is not Success success)
                return;
            
            MoneySpent?.Invoke(success.MoneySpent);
            LivesRestored?.Invoke(success.LivesRestored);
        }
    }
}