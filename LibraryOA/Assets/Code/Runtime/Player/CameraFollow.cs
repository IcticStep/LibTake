using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.Player
{
    internal sealed class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _offset;
        
        private Transform _target;
        private Transform _transform;

        private void Awake() =>
            _transform = transform;

        private void LateUpdate()
        {
            if (_target != null)
            {
                _transform.position = _target.position + _offset;
            }
        }

        public void SetTarget(Transform target) =>
            _target = target;
    }
}