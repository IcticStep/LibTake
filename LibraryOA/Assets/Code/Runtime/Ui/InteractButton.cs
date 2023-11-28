using System.Collections;
using Code.Runtime.Logic.Player;
using Code.Runtime.Services.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui
{
    internal sealed class InteractButton : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        [SerializeField]
        private Button _button;
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
            _button.onClick.AddListener(Interact);
            _updateCoroutine = StartCoroutine(UpdateViewInInterval());
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Interact);
            StopCoroutine(_updateCoroutine);
        }

        private IEnumerator UpdateViewInInterval()
        {
            while(true)
            {
                UpdateView();
                yield return _waitForSeconds;
            }
        }

        private void Interact() =>
            PlayerInteractor.InteractIfPossible();

        private void UpdateView()
        {
            if(PlayerInteractor.CanInteract())
                _smoothFader.UnFade();
            else
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