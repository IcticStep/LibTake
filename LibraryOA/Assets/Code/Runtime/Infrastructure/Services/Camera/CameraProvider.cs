using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.Services.Camera
{
    [UsedImplicitly]
    internal sealed class CameraProvider : ICameraProvider
    {
        public UnityEngine.Camera MainCamera { get; private set; }

        public void Initialize(UnityEngine.Camera main) =>
            MainCamera = main;

        public void CleanUp() =>
            MainCamera = null;
    }
}