using Code.Runtime.Logic.CameraControl;

namespace Code.Runtime.Infrastructure.Services.Camera
{
    internal interface ICameraProvider
    {
        CameraFollow CameraFollow { get; }
        UnityEngine.Camera MainCamera { get; }
        void Initialize(CameraFollow main);
        void CleanUp();
    }
}