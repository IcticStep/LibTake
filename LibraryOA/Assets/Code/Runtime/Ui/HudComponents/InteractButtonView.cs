using System.Collections;
using Code.Runtime.Logic.Player;
using Code.Runtime.Services.Player.Provider;
using Code.Runtime.Ui.Common;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.HudComponents
{
    internal sealed class InteractButtonView : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        [SerializeField]
        private float _updateInterval = 0.1f;

        private Coroutine _updateCoroutine;
        private IPlayerProviderService _playerProviderService;
        private WaitForSeconds _waitForSeconds;

        private PlayerInteractor PlayerInteractor => _playerProviderService.PlayerInteractor;
        
        [Inject]
        private void Construct(IPlayerProviderService playerProviderService) =>
            _playerProviderService = playerProviderService;

        private void Awake() =>
            _waitForSeconds = new WaitForSeconds(_updateInterval);

        private void Start()
        {
            UpdateViewImmediately();
            _updateCoroutine = StartCoroutine(UpdateViewInInterval());
        }

        private void OnDestroy() =>
            StopCoroutine(_updateCoroutine);

        private IEnumerator UpdateViewInInterval()
        {
            while(true)
            {
                UpdateView();
                yield return _waitForSeconds;
            }
        }

        private void UpdateView()
        {
            if(_smoothFader.AnimationInProgress)
                return;
            
            if(PlayerInteractor.CanInteract() && !_smoothFader.IsFullyVisible)
            {
                _smoothFader.UnFade();
                return;
            }
            
            if(!PlayerInteractor.CanInteract() && !_smoothFader.IsFullyInvisible)
                _smoothFader.Fade();
        }
        
        private void UpdateViewImmediately()
        {
            if(PlayerInteractor.CanInteract())
                _smoothFader.UnFadeImmediately();
            else
                _smoothFader.FadeImmediately();
        }
    }
}