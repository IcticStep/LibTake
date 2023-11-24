using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Runtime.Logic.Customers
{
    internal sealed class CustomerNavigator : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent _navMeshAgent;
        [SerializeField]
        private float _updateInterval = 0.1f;
        [SerializeField]
        private float _destinationReachedDistance;

        private readonly Queue<Vector3> _destinations = new();

        private WaitForSeconds _waitForInterval;
        private Transform _transform;
        private Coroutine _moveToDestinationCoroutine;
        private Vector3? _currentDestination;

        public event Action PointReached;
        public event Action LastPointReached;

        private void Awake()
        {
            _waitForInterval = new WaitForSeconds(_updateInterval);
            _transform = transform;
        }

        private void OnEnable() =>
            _moveToDestinationCoroutine ??= StartCoroutine(UpdateDestination());

        public void OnDisable()
        {
            StopCoroutine(_moveToDestinationCoroutine);
            _moveToDestinationCoroutine = null;
        }

        public void SetDestination(Vector3 destination) =>
            SetDestination(new[] { destination });

        public void SetDestination(IEnumerable<Vector3> destinations)
        {
            _destinations.Clear();
            foreach(Vector3 destination in destinations)
                _destinations.Enqueue(destination);
        }

        private IEnumerator UpdateDestination()
        {
            while(true)
            {
                if(_destinations.Count == 0 && _currentDestination is null)
                {
                    yield return _waitForInterval;
                    continue;
                }

                if(_currentDestination is null)
                    SetFirstDestination();

                if(DestinationIsReached(_currentDestination.Value))
                    SetNextDestination();

                yield return _waitForInterval;
            }
        }

        private void SetNextDestination()
        {
            if(!_destinations.Any())
            {
                _currentDestination = null;
                NotifyReached();
                return;
            }

            _currentDestination = _destinations.Dequeue();
            _navMeshAgent.SetDestination(_currentDestination.Value);
        }

        private void SetFirstDestination()
        {
            _currentDestination = _destinations.Dequeue();
            _navMeshAgent.SetDestination(_currentDestination.Value);
        }

        private void NotifyReached()
        {
            PointReached?.Invoke();
            if(!_destinations.Any())
                LastPointReached?.Invoke();
        }

        private bool DestinationIsReached(Vector3 destination)
        {
            float distance = Vector3.Distance(destination.WithY(0), _transform.position.WithY(0));
            return distance < _destinationReachedDistance;
        }
    }
}