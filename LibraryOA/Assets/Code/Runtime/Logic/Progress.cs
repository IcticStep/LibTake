using System;
using System.Threading;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.Logic
{
    public sealed class Progress : MonoBehaviour, ISavedProgress
    {
        private string _id;
        private float _timeToFinish;
        private float _value;
        private UniTask? _fillingTask;
        private CancellationTokenSource _cancellationTokenSource;
        private UniTaskCompletionSource _externalTaskSource;

        public bool Empty => Value == 0;
        public bool Full => Value >= 1;
        public bool Running => _fillingTask is not null;
        public bool CanBeStarted => !Running && !Full;
        public float MaxValue { get; private set; } = 1;
        public bool JustReset { get; private set; }
        public UniTask Task => _externalTaskSource.Task;
        
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
        public Action Started;
        [FormerlySerializedAs("Finished")]
        public Action Stopped;

        public void Reset()
        {
            JustReset = true;
            _externalTaskSource = null;
            StopFilling();
            Value = 0;
        }

        public void OnDisable() =>
            JustReset = false;

        public void Initialize(float timeToFinish) =>
            Initialize(null, timeToFinish);

        public void Initialize(string ownerId, float timeToFinish)
        {
            _externalTaskSource = new UniTaskCompletionSource();
            JustReset = false;
            _id = ownerId;
            _timeToFinish = timeToFinish;
        }

        public void LoadProgress(Data.Progress.Progress progress)
        {
            if(_id is null)
                return;
            
            Value = progress.WorldData.ProgressesStates.GetDataForId(_id);
        }

        public void UpdateProgress(Data.Progress.Progress progress)
        {
            if(_id is null)
                return;

            progress.WorldData.ProgressesStates.SetDataForId(_id, Value);
        }

        public void StartFilling(Action onFinishCallback = null)
        {
            JustReset = false;
            _cancellationTokenSource = new CancellationTokenSource();
            _fillingTask = Fill(onFinishCallback, _cancellationTokenSource.Token);
            Started?.Invoke();
            _fillingTask.Value.Forget();
        }

        public void StopFilling()
        {
            if(!Running)
                return;
            
            _cancellationTokenSource.Cancel();
            _fillingTask = null;
            Stopped?.Invoke();
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
            _externalTaskSource?.TrySetResult();
            _externalTaskSource = null;
            onFinishCallback?.Invoke();
            Stopped?.Invoke();
        }

        private float CalculateFillingAmount() =>
            Time.deltaTime / _timeToFinish;
    }
}