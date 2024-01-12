using UnityEngine;

namespace Code.Runtime.Logic.Triggers
{
    internal sealed class SwitchSecondFloorTrigger : MonoBehaviour
    {
        [SerializeField]
        private Library _library;
        [SerializeField]
        private bool _targetState;
        [SerializeField]
        private Trigger _trigger;

        private void Awake() =>
            _trigger.Entered += UpdateFloorState;
        
        private void OnDestroy() =>
            _trigger.Entered -= UpdateFloorState;

        private void UpdateFloorState()
        {
            if(_targetState)
                _library.ShowSecondFloor();
            else
                _library.HideSecondFloor();
        }
    }
}