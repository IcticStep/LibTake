using Code.Runtime.Ui.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Ui.Menu.GlobalGoals
{
    internal sealed class GlobalGoalsContainer : MonoBehaviour
    {
        [SerializeField]
        private FastFlyingFading _fader;

        public float Duration => _fader.Duration;
        
        private void Start() =>
            _fader.SetInOffScreenPosition();

        public UniTask Show() =>
            _fader.FadeIn(this.GetCancellationTokenOnDestroy());

        public UniTask Hide() =>
            _fader.FadeOut(this.GetCancellationTokenOnDestroy());
    }
}