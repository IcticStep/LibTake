using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic.CameraControl
{
    internal interface ICameraFollow
    {
        Camera Camera { get; }
        void MoveToNewTarget(Transform target);
        UniTaskVoid MoveToNewTargetAsync(Transform target, float duration);
        void MoveToNewTarget(Transform target, float duration);
    }
}