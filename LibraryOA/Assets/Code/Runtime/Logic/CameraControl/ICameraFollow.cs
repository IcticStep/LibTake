using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic.CameraControl
{
    internal interface ICameraFollow
    {
        Camera Camera { get; }
        void MoveToNewTarget(Transform target);
        UniTask MoveToNewTargetAsync(Transform target, float duration);
        void MoveToNewTarget(Transform target, float duration);
    }
}