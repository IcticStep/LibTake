using System;
using Code.Runtime.Logic.Interactables;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Ui.Coin.Triggers
{
    internal sealed class InteractableFlyResourceTrigger : MonoBehaviour
    {
        [SerializeField]
        private Interactable _interactable;
        [SerializeField]
        private FlyingResource _flyingResource;
        [SerializeField]
        private float _delaySeconds;

        private void Awake()
        {
            if(_interactable is not IRewardSource rewardSource)
                throw new InvalidOperationException($"{nameof(InteractableFlyResourceTrigger)} requires {nameof(IRewardSource)} implementation in {nameof(Interactable)}.");

            rewardSource.Rewarded += ShowResource;
        }

        private void OnDestroy()
        {
            if(_interactable is IRewardSource coinRewardSource)
                coinRewardSource.Rewarded -= ShowResource;
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