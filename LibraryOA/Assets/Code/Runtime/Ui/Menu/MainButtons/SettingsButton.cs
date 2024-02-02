using Code.Runtime.Data;
using Code.Runtime.Infrastructure.GameStates;
using Code.Runtime.Infrastructure.GameStates.States;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Services.Loading;
using Code.Runtime.Ui.Menu.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui.Menu.MainButtons
{
    internal sealed class SettingsButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private MenuGroup _mainButtonsGroup;
        [SerializeField]
        private MenuGroup _settingsGroup;
        
        private void Awake() =>
            _button.onClick.AddListener(OnContinueButtonPressed);
        
        private void OnDestroy() =>
            _button.onClick.RemoveListener(OnContinueButtonPressed);
        
        private void OnContinueButtonPressed() =>
            ShowSettings()
                .Forget();

        private async UniTaskVoid ShowSettings() =>
            await UniTask.WhenAll(_mainButtonsGroup.Hide(), _settingsGroup.Show());
    }
}