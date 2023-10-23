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
        public float Value { get; private set; }
        private string _id;
        private float _timeToFinish;
        private UniTask? _fillingTask;
        private CancellationTokenSource _cancellationTokenSource;

        public bool Empty => Value == 0;
        public bool Full => Value >= 1;
        public bool InProgress => _fillingTask is not null;
        public bool CanBeStarted => !InProgress && !Full; 

        public void Initialize(string ownerId, float timeToFinish)
        {
            _id = ownerId;
            _timeToFinish = timeToFinish;
        }
        
        public void LoadProgress(PlayerProgress progress) =>
            throw new System.NotImplementedException();

        public void UpdateProgress(PlayerProgress progress) =>
            throw new System.NotImplementedException();

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
                
                Value += CalculateFillingAmount(Time.deltaTime);
            }

            _fillingTask = null;
            _cancellationTokenSource = null;
            onFinishCallback.Invoke();
        }

        private float CalculateFillingAmount(float deltaTime) =>
            _timeToFinish / deltaTime;
    }
}