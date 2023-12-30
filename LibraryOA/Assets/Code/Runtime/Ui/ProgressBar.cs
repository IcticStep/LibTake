using Code.Runtime.Ui.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.Ui
{
    internal sealed class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Image _target;
        [SerializeField]
        private SmoothFader _smoothFader;
        [SerializeField]
        private float _visualMinimum = 0.01f;
        [SerializeField]
        private bool _invertedFilling;
        
        public void SetProgress(float value, float maxValue)
        {
            _target.fillAmount = GetFillAmount(value, maxValue);
            
            if(_smoothFader != null)
                SetVisibility(value, maxValue);
        }

        private float GetFillAmount(float value, float maxValue) =>
            _invertedFilling
                ? 1 - value / maxValue
                :  value / maxValue;

        private void SetVisibility(float value, float maxValue)
        {
            bool shouldBeVisible = ValueIsMoreThanVisualMinimum(value, maxValue);
            if(shouldBeVisible && !_smoothFader.IsFullyVisible)
                _smoothFader.UnFade();
            else if(!shouldBeVisible && !_smoothFader.IsFullyInvisible)
                _smoothFader.Fade();
        }

        private bool ValueIsMoreThanVisualMinimum(float value, float maxValue) =>
            value / maxValue >= _visualMinimum;
    }
}