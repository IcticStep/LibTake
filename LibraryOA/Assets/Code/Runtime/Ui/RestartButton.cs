using Code.Runtime.Infrastructure.Services.Restart;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui
{
    internal sealed class RestartButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        
        private IRestartService _restartService;

        [Inject]
        private void Construct(IRestartService restartService) =>
            _restartService = restartService;

        private void Awake() =>
            _button.onClick.AddListener(Restart);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(Restart);

        private void Restart() =>
            _restartService.Restart();
    }
}