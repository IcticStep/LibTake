using System;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Logic
{
    public interface IProgress
    {
        bool Empty { get; }
        bool Full { get; }
        bool Running { get; }
        bool CanBeStarted { get; }
        float MaxValue { get; }
        bool JustReset { get; }
        UniTask Task { get; }
        float Value { get; }
        
        void Reset();
        void Initialize(float timeToFinish);
        void Initialize(string ownerId, float timeToFinish);
        void StartFilling(Action onFinishCallback = null);
        void StopFilling();
    }
}