using System.Threading;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic
{
    internal sealed class Progress : MonoBehaviour, ISavedProgress
    {
        public float Value { get; private set; } = 0;
        private string _id;
        private float _timeToFinish;
        private UniTask? _fillingTask;

        public bool Empty => Value == 0;
        public bool Full => Value >= 1;

        public void Initialize(string ownerId, float timeToFinish)
        {
            _id = ownerId;
            _timeToFinish = timeToFinish;
        }
        
        public void LoadProgress(PlayerProgress progress) =>
            throw new System.NotImplementedException();

        public void UpdateProgress(PlayerProgress progress) =>
            throw new System.NotImplementedException();

        public void StartFilling()
        {
            _fillingTask = Fill();
            _fillingTask.Value.Forget();
        }

        private async UniTask Fill()
        {
            while(!Full)
            {
                await UniTask.NextFrame();
                Value += CalculateFillingAmount(Time.deltaTime);
            }
        }

        private float CalculateFillingAmount(float deltaTime) =>
            _timeToFinish / deltaTime;
    }
}