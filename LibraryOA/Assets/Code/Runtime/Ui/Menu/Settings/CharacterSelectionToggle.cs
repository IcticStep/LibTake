using System;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.StaticData.CharacterSelection;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui.Menu.Settings
{
    internal sealed class CharacterSelectionToggle : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private Toggle _toggle;
        
        private IStaticDataService _staticDataService;
        private ISaveLoadService _saveLoadService;
        public CharacterTypeId CharacterType { get; private set; }

        [Inject]
        private void Construct(IStaticDataService staticDataService, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }

        private void Awake() =>
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);

        private void OnDestroy() =>
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);

        private void OnValidate() =>
            _toggle ??= GetComponentInChildren<Toggle>();

        public void Initialize(CharacterTypeId availableCharacter)
        {
            CharacterType = availableCharacter;
            CharacterConfig config = _staticDataService.ForCharacter(availableCharacter);
            _icon.sprite = config.Icon;
        }

        public void Select() =>
            _toggle.isOn = true;

        private void OnToggleValueChanged(bool value)
        {
            if(value)
                _saveLoadService.SaveCharacterSelected(CharacterType);
        }
    }
}