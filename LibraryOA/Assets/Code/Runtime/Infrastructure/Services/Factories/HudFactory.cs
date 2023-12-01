using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Infrastructure.Services.HudProvider;
using Code.Runtime.Services.InputService;
using Code.Runtime.Services.InputService.JoystickPack;
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

        public HudFactory(IInputService inputService, IAssetProvider assetProvider, IHudProviderService hudProviderService)
        {
            _inputService = inputService;
            _assetProvider = assetProvider;
            _hudProviderService = hudProviderService;
        }

        public void Create()
        {
            GameObject hud = _assetProvider.Instantiate(AssetPath.Hud);
            Joystick joystick = hud.GetComponentInChildren<Joystick>();
            _inputService.RegisterMovementProvider(new JoystickPackWrapper(joystick));
            _hudProviderService.RegisterHud(hud);
        }
    }
}