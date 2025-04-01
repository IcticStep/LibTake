using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Logic.CameraControl;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui.Menu.Settings
{
    internal sealed class CameraSettingsToggle : MonoBehaviour
    {
        [SerializeField]
        private Toggle _toggle;

        [SerializeField]
        private Image _lowFlag;
        
        [SerializeField]
        private Image _highFlag;
        
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService) =>
            _saveLoadService = saveLoadService;

        private void OnValidate() =>
            _toggle ??= GetComponent<Toggle>();

        private void Awake()
        {
            CameraTypeId cameraCurrentSettings = _saveLoadService.LoadCameraSettings();
            UpdateToggle(cameraCurrentSettings);
            
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnDestroy() =>
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);

        private void OnToggleValueChanged(bool value)
        {
            CameraTypeId settings = value ? CameraTypeId.High : CameraTypeId.Low;
            _saveLoadService.SaveCameraSettings(settings);
            UpdateToggle(settings);
        }

        private void UpdateToggle(CameraTypeId cameraCurrentSettings)
        {
            _toggle.isOn = cameraCurrentSettings is CameraTypeId.High;
            _lowFlag.enabled = cameraCurrentSettings is CameraTypeId.Low;
            _highFlag.enabled = cameraCurrentSettings is CameraTypeId.High;
        }
    }
}