using System;
using UnityEngine;

namespace Code.Runtime.Logic.Triggers
{
    [RequireComponent(typeof(BoxCollider))]
    internal sealed class Trigger : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider _collider;
        [SerializeField]
        private Color32 _color;

        public event Action Entered;
        public event Action Exited;

        private void OnTriggerEnter(Collider other) =>
            Entered?.Invoke();

        private void OnTriggerExit(Collider other) =>
            Exited?.Invoke();

        private void OnValidate() =>
            _collider ??= GetComponent<BoxCollider>();

        private void OnDrawGizmos()
        {
            if(!_collider) 
                return;

            Gizmos.color = _color;
            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
        }
    }
}