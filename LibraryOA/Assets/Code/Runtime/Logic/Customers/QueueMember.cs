using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic.Customers
{
    public sealed class QueueMember : MonoBehaviour
    {
        [SerializeField]
        private float _minDistanceToAchievePoint = 1f;
        [SerializeField]
        private float _reachGoalCheckingInterval = 0.1f;

        private Transform _transform;
        public Vector3? CurrentPoint { get; private set; }
        public bool ActiveMember => CurrentPoint is not null;
        public event Action Updated;
        public event Action BecameFirst;

        private void Awake() =>
            _transform = transform;

        private void Reset() =>
            CurrentPoint = null;

        public void UpdatePoint(Vector3? point)
        {
            CurrentPoint = point;
            Updated?.Invoke();
        }

        public async void InformBecameFirst()
        {
            await WaitUntilArriveToPoint();
            if(!ActiveMember)
                return;
            
            BecameFirst?.Invoke();
        }

        private async UniTask WaitUntilArriveToPoint()
        {
            do
            {
                await UniTask.WaitForSeconds(_reachGoalCheckingInterval);
            } while(OnTheWayToPoint());
            return;

            bool OnTheWayToPoint() =>
                ActiveMember
                // ReSharper disable once PossibleInvalidOperationException
                && Vector3.Distance(CurrentPoint.Value, _transform.position) > _minDistanceToAchievePoint;
        }
    }
}