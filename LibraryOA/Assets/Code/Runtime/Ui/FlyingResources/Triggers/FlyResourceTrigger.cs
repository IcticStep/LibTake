using System;
using Code.Runtime.Logic.Interactables;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.Ui.FlyingResources.Triggers
{
    internal sealed class FlyResourceTrigger : MonoBehaviour
    {
        [SerializeField]
        private MonoBehaviour _rewardSource;
        [SerializeField]
        private FlyingResource _flyingResource;
        [SerializeField]
        private float _delaySeconds;

        private void Awake()
        {
            if(_rewardSource is IRewardSource rewardSource)
                rewardSource.Rewarded += ShowResource;
        }

        private void OnDestroy()
        {
            if(_rewardSource is IRewardSource coinRewardSource)
                coinRewardSource.Rewarded -= ShowResource;
        }

        private void OnValidate()
        {
            if(_rewardSource is null)
                return;

            if(_rewardSource is IRewardSource)
                return;

            _rewardSource = null;
            throw new InvalidOperationException($"{nameof(FlyResourceTrigger)} requires {nameof(IRewardSource)} implementation in {nameof(_rewardSource)} field.");
        }

        private void ShowResource(int amount) =>
            ShowResourceAsync(amount)
                .Forget();

        private async UniTaskVoid ShowResourceAsync(int amount)
        {
            await UniTask.WaitForSeconds(_delaySeconds);
            _flyingResource
                .FlyResource(amount)
                .Forget();
        }
    }
}