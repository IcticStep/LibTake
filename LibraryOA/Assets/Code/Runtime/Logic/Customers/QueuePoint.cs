using System;
using UnityEngine;

namespace Code.Runtime.Logic.Customers
{
    public sealed class QueuePoint : MonoBehaviour
    {
        private Transform _transform;
        public Vector3 Position => _transform.position;

        private void Awake() =>
            _transform = transform;
    }
}