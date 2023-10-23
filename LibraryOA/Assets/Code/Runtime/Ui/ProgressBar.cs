using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.Ui
{
    internal sealed class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Image _target;

        public void SetProgress(float value, float maxValue) =>
            _target.fillAmount = value / maxValue;
    }
}