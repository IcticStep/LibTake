using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Services.Interactions.Truck;
using Code.Runtime.Ui.Common;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.Tutorial
{
    internal sealed class TruckHint : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        
        private ITruckInteractionService _truckInteractionService;
        private IPersistantProgressService _persistantProgressService;

        private TutorialData ProgressTutorialData => _persistantProgressService.Progress.TutorialData;

        [Inject]
        private void Construct(ITruckInteractionService truckInteractionService, IPersistantProgressService persistantProgressService)
        {
            _persistantProgressService = persistantProgressService;
            _truckInteractionService = truckInteractionService;
        }

        private void Awake() =>
            _truckInteractionService.BooksTaken += OnBooksTaken;

        private void Start()
        {
            if(ProgressTutorialData.TruckHintShown)
            {
                _smoothFader.FadeImmediately();
                return;
            }
            
            _smoothFader.UnFade();
            ProgressTutorialData.TruckHintShown = true;
        }

        private void OnDestroy() =>
            _truckInteractionService.BooksTaken -= OnBooksTaken;

        private void OnBooksTaken()
        {
            if(_smoothFader.IsFullyVisible)
                _smoothFader.Fade();
        }
    }
}