using Code.Runtime.Ui.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Ui.Menu
{
    internal sealed class GlobalGoalsContainer : MonoBehaviour
    {
        [SerializeField]
        private FastFlyingFading _fader;

        private void Start() =>
            _fader.SetInOffScreenPosition();

        public UniTask Show() =>
            _fader.FadeIn(this.GetCancellationTokenOnDestroy());

        public UniTask Hide() =>
            _fader.FadeOut(this.GetCancellationTokenOnDestroy());
    }
}