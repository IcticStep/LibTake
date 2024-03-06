using Code.Runtime.Infrastructure.Settings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui.Menu.Settings
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(ButtonSwitcherView))]
    internal sealed class SoundsButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private ButtonSwitcherView _buttonSwitcherView;
        
        private ISettingsService _settingsService;

        [Inject]
        private void Construct(ISettingsService settingsService) =>
            _settingsService = settingsService;

        private void OnValidate()
        {
            _button ??= GetComponent<Button>();
            _buttonSwitcherView ??= GetComponent<ButtonSwitcherView>();
        }

        private void Start() =>
            UpdateView();

        private void Awake() =>
            _button.onClick.AddListener(OnButtonClicked);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(OnButtonClicked);

        private void OnButtonClicked()
        {
            ToggleMusic();
            UpdateView();
        }

        private void ToggleMusic()
        {
            if(_settingsService.SfxEnabled)
                _settingsService.TurnOffSfx();
            else
                _settingsService.TurnOnSfx();
        }

        private void UpdateView()
        {
            if(_settingsService.SfxEnabled)
                _buttonSwitcherView.SetEnabledView();
            else
                _buttonSwitcherView.SetDisabledView();
        }
    }
}