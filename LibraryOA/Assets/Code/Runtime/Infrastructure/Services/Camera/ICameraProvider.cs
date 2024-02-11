using Code.Runtime.Logic.CameraControl;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Camera
{
    internal interface ICameraProvider
    {
        CameraFollow CameraFollow { get; }
        UnityEngine.Camera MainCamera { get; }
        void Initialize(CameraFollow main);
        void EnableAnimator();
        void DisableAnimator();
        void DisableFollow();
        void StartLookingAfter(Transform target);
        void StopLookingAfter();
        void CleanUp();
    }
}