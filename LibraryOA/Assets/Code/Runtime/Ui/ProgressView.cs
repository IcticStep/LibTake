using Code.Runtime.Logic;
using UnityEngine;

namespace Code.Runtime.Ui
{
    internal sealed class ProgressView : MonoBehaviour
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

        private void UpdateProgressBar(float value) =>
            _progressBar.SetProgress(value, _progress.MaxValue);
    }
}