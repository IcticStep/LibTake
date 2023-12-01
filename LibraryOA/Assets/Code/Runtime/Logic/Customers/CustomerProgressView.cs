using Code.Runtime.Ui;
using UnityEngine;

namespace Code.Runtime.Logic.Customers
{
    internal sealed class CustomerProgressView : MonoBehaviour
    {
        [SerializeField]
        private Progress _progress;
        
        [SerializeField]
        private ProgressBar _progressBar;

        private void Start()
        { 
            _progress.Updated += UpdateProgressBar;
            UpdateProgressBar(_progress.Value);
        }

        private void OnDestroy() =>
            _progress.Updated -= UpdateProgressBar;

        private void UpdateProgressBar(float value)
        {
            if(_progress.JustReset)
                return;
            
            _progressBar.SetProgress(value, _progress.MaxValue);
        }
    }
}