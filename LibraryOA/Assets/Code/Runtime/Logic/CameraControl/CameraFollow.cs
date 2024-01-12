using UnityEngine;

namespace Code.Runtime.Logic.CameraControl
{
    internal sealed class CameraFollow : MonoBehaviour, ICameraFollow
    {
        [SerializeField]
        private Vector3 _offset;
        
        private Transform _target;
        private Transform _transform;
        
        public Camera Camera { get; private set; }

        private void Awake()
        {
            _transform = transform;
            Camera = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            if (_target != null)
                _transform.position = _target.position + _offset;
        }

        public void SetTarget(Transform target) =>
            _target = target;
    }
}