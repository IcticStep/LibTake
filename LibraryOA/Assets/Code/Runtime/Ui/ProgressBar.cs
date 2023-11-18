using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.Ui
{
    internal sealed class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Image _target;
        [SerializeField]
        private Image _background;
        [SerializeField]
        private float _visualMinimum = 0.01f;

        public void SetProgress(float value, float maxValue)
        {
            _target.fillAmount = value / maxValue;
            SetVisibility(value, maxValue);
        }

        private void SetVisibility(float value, float maxValue)
        {
            bool shouldBeVisible = ValueIsMoreThanVisualMinimum(value, maxValue);
            _background.enabled = shouldBeVisible;
            _target.enabled = shouldBeVisible;
        }

        private bool ValueIsMoreThanVisualMinimum(float value, float maxValue) =>
            value / maxValue >= _visualMinimum;
    }
}