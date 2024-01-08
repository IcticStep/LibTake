using System;
using Code.Runtime.Logic.Interactables.Api;
using Code.Runtime.Services.Interactions.BookSlotInteraction;
using Code.Runtime.Services.Interactions.ScannerInteraction;
using Code.Runtime.Ui;
using Code.Runtime.Ui.Coin;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactables
{
    internal sealed class Scanner : Interactable, IHoverStartListener, IHoverEndListener, IProgressOwner, IRewardSource
    {
        [SerializeField] 
        private BookStorage _bookStorageObject;
        [SerializeField]
        private Progress _progress;
        [SerializeField]
        private BookUi _bookUi;

        private IScannerInteractionService _scannerInteractionService;
        private IBookSlotInteractionService _bookSlotInteractionService;

        public bool InProgress => _progress.Running;
        public event Action<int> Rewarded;

        [Inject]
        private void Construct(IScannerInteractionService scannerInteractionService, IBookSlotInteractionService bookSlotInteractionService)
        {
            _scannerInteractionService = scannerInteractionService;
            _bookSlotInteractionService = bookSlotInteractionService;
        }

        public override bool CanInteract() =>
            _bookSlotInteractionService.CanInteract(_bookStorageObject);

        public override void Interact()
        {
            _bookSlotInteractionService.Interact(_bookStorageObject);
            _scannerInteractionService.Interact(_bookStorageObject, _progress, this);
            
            if(_bookStorageObject.HasBook)
                _bookUi.ShowData();
            else
                _bookUi.HideData();
        }

        public void OnHoverStart()
        {
            _scannerInteractionService.StartScanningIfPossible(_bookStorageObject, _progress, this);
            _bookUi.ShowData();
        }

        public void OnHoverEnd()
        {
            _scannerInteractionService.StopReading(_progress);
            _bookUi.HideData();
        }

        public void NotifyRewarded(int reward) =>
            Rewarded?.Invoke(reward);
    }
}