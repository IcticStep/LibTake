using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Infrastructure.Services.Hud;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.InputService;
using Code.Runtime.Services.InputService.JoystickPack;
using Code.Runtime.StaticData.Ui;
using Code.Runtime.Ui.Messages;
using External_Dependencies.Joystick_Pack.Scripts.Base;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories
{
    [UsedImplicitly]
    internal sealed class HudFactory : IHudFactory
    {
        private readonly IInputService _inputService;
        private readonly IAssetProvider _assetProvider;
        private readonly IHudProviderService _hudProviderService;
        private readonly IStaticDataService _staticDataService;

        public HudFactory(IInputService inputService, IAssetProvider assetProvider, IHudProviderService hudProviderService,
            IStaticDataService staticDataService)
        {
            _inputService = inputService;
            _assetProvider = assetProvider;
            _hudProviderService = hudProviderService;
            _staticDataService = staticDataService;
        }

        public void Create()
        {
            GameObject hud = _assetProvider.Instantiate(AssetPath.Hud);
            Canvas mainCanvas = hud.GetComponent<Canvas>();
            
            SetUpJoystick(hud);
            SetUpMessages(hud);

            _hudProviderService.RegisterHud(hud, mainCanvas);
        }

        private void SetUpMessages(GameObject hud)
        {
            UiData uiData = _staticDataService.Ui;
            MorningMessage morningMessage = hud.GetComponentInChildren<MorningMessage>();
            morningMessage.ConfigureTimings(uiData.MorningMessageIntervals);
            DayMessage dayMessage = hud.GetComponentInChildren<DayMessage>();
            dayMessage.ConfigureTimings(uiData.DayMessageIntervals);
        }

        private void SetUpJoystick(GameObject hud)
        {
            Joystick joystick = hud.GetComponentInChildren<Joystick>();
            _inputService.RegisterMovementProvider(new JoystickPackWrapper(joystick));
        }
    }
}