using UnityEngine;

namespace Code.Runtime.Logic.CameraControl
{
    internal interface ICameraFollow
    {
        Camera Camera { get; }
        void SetTarget(Transform target);
    }
}