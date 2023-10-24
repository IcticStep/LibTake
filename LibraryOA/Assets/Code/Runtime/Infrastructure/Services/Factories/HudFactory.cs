using Code.Runtime.Infrastructure.AssetManagement;
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

        public HudFactory(IInputService inputService, IAssetProvider assetProvider)
        {
            _inputService = inputService;
            _assetProvider = assetProvider;
        }

        public void Create()
        {
            GameObject hud = _assetProvider.Instantiate(AssetPath.Hud);
            Joystick joystick = hud.GetComponentInChildren<Joystick>();
            _inputService.RegisterMovementProvider(new JoystickPackWrapper(joystick));
        }
    }
}