using Code.Runtime.Logic;
using UnityEngine;

namespace Code.Runtime.Ui
{
    internal sealed class OnProgressFinishBookUiUpdater : MonoBehaviour
    {
        [SerializeField]
        private Progress _progress;

        [SerializeField]
        private BookUi _bookUi;

        private void Awake() =>
            _progress.Finished += UpdateBookUi;

        private void OnDestroy() =>
            _progress.Finished -= UpdateBookUi;

        private void UpdateBookUi() =>
            _bookUi.ShowData();
    }
}