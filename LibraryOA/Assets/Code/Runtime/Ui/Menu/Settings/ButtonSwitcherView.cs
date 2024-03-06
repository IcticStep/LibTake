using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.Ui.Menu.Settings
{
    [RequireComponent(typeof(Image))]
    internal sealed class ButtonSwitcherView : MonoBehaviour
    {
        [SerializeField]
        private Image _button;
        [SerializeField]
        private Color _disabledColor = new(0.39f, 0.39f, 0.39f);
        [SerializeField]
        private Color _enabledColor = new(1, 1, 1);
        [SerializeField]
        private float _animationTime = 0.5f;
        [SerializeField]
        private Ease _ease = Ease.OutQuad;
        
        private Tweener _tweener;
        
        private void OnValidate() =>
            _button ??= GetComponent<Image>();

        public void SetDisabledView()
        {
            KillTweenerIfAny();
            _tweener = _button
                .DOColor(_disabledColor, _animationTime)
                .SetEase(_ease)
                .OnComplete(KillTweenerIfAny);

            _tweener.ToUniTask(cancellationToken: this.GetCancellationTokenOnDestroy());
        }

        public void SetEnabledView()
        {
            KillTweenerIfAny();
            _tweener = _button
                .DOColor(_enabledColor, _animationTime)
                .SetEase(_ease)
                .OnComplete(KillTweenerIfAny);
            
            _tweener.ToUniTask(cancellationToken: this.GetCancellationTokenOnDestroy());
        }

        private void KillTweenerIfAny()
        {
            _tweener?.Kill();
            _tweener = null;
        }
    }
}