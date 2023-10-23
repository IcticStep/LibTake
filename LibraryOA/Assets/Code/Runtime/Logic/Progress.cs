using System;
using System.Threading;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic
{
    internal sealed class Progress : MonoBehaviour, ISavedProgress
    {
        private string _id;
        private float _timeToFinish;
        private float _value;
        private UniTask? _fillingTask;
        private CancellationTokenSource _cancellationTokenSource;

        public bool Empty => Value == 0;
        public bool Full => Value >= 1;
        public bool InProgress => _fillingTask is not null;
        public bool CanBeStarted => !InProgress && !Full;
        public float MaxValue { get; private set; } = 1;
        
        public float Value
        {
            get => _value;
            private set
            {
                _value = value;
                Updated?.Invoke(Value);
            }
        }

        public Action<float> Updated;

        public void Initialize(string ownerId, float timeToFinish)
        {
            _id = ownerId;
            _timeToFinish = timeToFinish;
        }
        
        public void LoadProgress(PlayerProgress progress) =>
            Value = progress.WorldData.ProgressesStates.GetDataForId(_id);

        public void UpdateProgress(PlayerProgress progress) =>
            progress.WorldData.ProgressesStates.SetDataForId(_id, Value);

        public void StartFilling(Action onFinishCallback)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _fillingTask = Fill(onFinishCallback, _cancellationTokenSource.Token);
            _fillingTask.Value.Forget();
        }

        public void Reset()
        {
            StopFilling();
            Value = 0;
        }

        public void StopFilling()
        {
            if(!InProgress)
                return;
            
            _cancellationTokenSource.Cancel();
            _fillingTask = null;
        }

        private async UniTask Fill(Action onFinishCallback, CancellationToken cancellationToken)
        {
            while(!Full)
            {
                await UniTask.NextFrame(cancellationToken);
                if(_cancellationTokenSource.IsCancellationRequested)
                    break;

                Value += CalculateFillingAmount();
            }

            _fillingTask = null;
            _cancellationTokenSource = null;
            onFinishCallback.Invoke();
        }

        private float CalculateFillingAmount() =>
            Time.deltaTime / _timeToFinish;
    }
}