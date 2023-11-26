using Code.Runtime.Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Logic.Triggers
{
    internal sealed class SwitchSecondFloorTrigger : MonoBehaviour
    {
        [SerializeField]
        private Library _library;
        [SerializeField]
        private bool _targetState;
        [SerializeField]
        private BoxCollider _collider;
        
        private void OnTriggerEnter(Collider other)
        {
            if(_targetState)
                _library.ShowSecondFloor();
            else
                _library.HideSecondFloor();
        }

        private void OnDrawGizmos()
        {
            if(!_collider) 
                return;
      
            Gizmos.color = _targetState 
                ? new Color32(255, 183, 50, 130) 
                : new Color32(80, 59, 59, 130);
            
            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
        }
    }
}