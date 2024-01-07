using System;
using Code.Runtime.Logic.Interactables;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Ui.Coin.Triggers
{
    internal sealed class InteractableCoinTrigger : MonoBehaviour
    {
        [SerializeField]
        private Interactable _interactable;
        [SerializeField]
        private FlyingCoin _flyingCoin;

        private void Awake()
        {
            if(_interactable is not ICoinRewardSource coinRewardSource)
                throw new InvalidOperationException($"{nameof(InteractableCoinTrigger)} requires {nameof(ICoinRewardSource)} implementation in {nameof(Interactable)}.");

            coinRewardSource.Rewarded += ShowCoin;
        }

        private void OnDestroy()
        {
            if(_interactable is ICoinRewardSource coinRewardSource)
                coinRewardSource.Rewarded -= ShowCoin;
        }

        private void ShowCoin(int amount) =>
            _flyingCoin
                .ShowCoin(amount)
                .Forget();
    }
}