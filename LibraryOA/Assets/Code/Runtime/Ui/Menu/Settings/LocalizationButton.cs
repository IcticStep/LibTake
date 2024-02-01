using Code.Runtime.Infrastructure.Locales;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui.Menu.Settings
{
    internal sealed class LocalizationButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        
        private ILocalizationService _localizationService;

        [Inject]
        private void Construct(ILocalizationService localizationService) =>
            _localizationService = localizationService;

        private void OnValidate() =>
            _button ??= GetComponent<Button>();
        
        private void Awake() =>
            _button.onClick.AddListener(OnLocalizationButton);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(OnLocalizationButton);

        private void OnLocalizationButton() =>
            _localizationService.SetNextLocale();
    }
}