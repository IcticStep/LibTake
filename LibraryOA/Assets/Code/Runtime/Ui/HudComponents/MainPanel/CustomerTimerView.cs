using Code.Runtime.Ui.Common.Progress;
using UnityEngine;

namespace Code.Runtime.Ui.HudComponents.MainPanel
{
    internal sealed class CustomerTimerView : MonoBehaviour
    {
        [SerializeField]
        private ProgressBar _progressBar;
        [SerializeField]
        private CustomersTimerSource _customersTimerSource;

        private void Awake() =>
            _customersTimerSource.Updated += OnCustomersTimerSourceUpdated;
        
        private void OnDestroy() =>
            _customersTimerSource.Updated -= OnCustomersTimerSourceUpdated;

        private void OnCustomersTimerSourceUpdated(float value) =>
            _progressBar.SetProgress(value, 1);
    }
}