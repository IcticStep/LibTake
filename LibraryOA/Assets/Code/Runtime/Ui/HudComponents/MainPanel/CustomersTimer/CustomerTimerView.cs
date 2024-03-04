using System;
using Code.Runtime.Data;
using Code.Runtime.Ui.Common.Progress;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Code.Runtime.Ui.HudComponents.MainPanel.CustomersTimer
{
    internal sealed class CustomerTimerView : MonoBehaviour
    {
        [SerializeField]
        private ProgressBar _progressBar;
        [SerializeField]
        private CustomersTimerSource _customersTimerSource;
        [SerializeField]
        private RectTransform _animationTarget;
        [SerializeField]
        private RangeFloat _animationScaleRange = new RangeFloat(1f, 1.1f);
        [SerializeField]
        private RangeFloat _animationDurationRange = new RangeFloat(0.1f, 0.5f);

        private Tweener _tweener;

        public event Action ClockTick;

        private void Awake() =>
            _customersTimerSource.Updated += OnCustomersTimerSourceUpdated;
        
        private void OnDestroy()
        {
            _customersTimerSource.Updated -= OnCustomersTimerSourceUpdated;
            KillTweenerIfAny();
        }

        private void OnCustomersTimerSourceUpdated(float value)
        {
            _progressBar.SetProgress(value, 1);

            Animate(value);
        }

        private void Animate(float value)
        {
            if(value == 0)
            {
                KillTweenerIfAny();
                return;
            }
            
            if(_tweener != null)
                return;

            Vector3 punch = Vector3.one * Mathf.Lerp(_animationScaleRange.Min, _animationScaleRange.Max, value);
            float duration = _animationDurationRange.Max + _animationDurationRange.Min - Mathf.Lerp(_animationDurationRange.Min, _animationDurationRange.Max, value);
            _tweener = _animationTarget
                .DOPunchScale(punch, duration, 1, 0.5f)
                .OnComplete(OnClockTickComplete);
                
            _tweener
                .ToUniTask(cancellationToken: this.GetCancellationTokenOnDestroy());
        }

        private void OnClockTickComplete()
        {
            ClockTick?.Invoke();
            KillTweenerIfAny();
        }

        private void KillTweenerIfAny()
        {
            if(_tweener == null)
                return;

            _tweener.Kill(complete: true);
            _tweener = null;
        }
    }
}