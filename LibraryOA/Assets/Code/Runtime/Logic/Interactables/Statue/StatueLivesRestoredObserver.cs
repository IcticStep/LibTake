using System;
using Code.Runtime.Ui.FlyingResources;
using UnityEngine;

namespace Code.Runtime.Logic.Interactables.Statue
{
    internal sealed class StatueLivesRestoredObserver : MonoBehaviour, IRewardSource
    {
        [SerializeField]
        private Statue _statue;

        public event Action<int> Rewarded;

        private void Awake() =>
            _statue.LivesRestored += NotifyRewarded;

        private void OnDestroy() =>
            _statue.LivesRestored -= NotifyRewarded;

        private void NotifyRewarded(int amount) =>
            Rewarded?.Invoke(amount);
    }
}