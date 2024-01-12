using Code.Runtime.Logic.CameraControl;
using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.Services.Camera
{
    [UsedImplicitly]
    internal sealed class CameraProvider : ICameraProvider
    {
        public CameraFollow CameraFollow { get; private set; }
        public UnityEngine.Camera MainCamera => CameraFollow.Camera;

        public void Initialize(CameraFollow main) =>
            CameraFollow = main;

        public void CleanUp() =>
            CameraFollow = null;
    }
}