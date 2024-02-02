using Code.Runtime.Ui.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.Ui.Menu.Common
{
    internal sealed class MenuGroup : MonoBehaviour
    {
        [SerializeField]
        private GraphicRaycaster _graphicRaycaster;
        [SerializeField]
        private FastFlyingFading _fader;

        public async UniTask Show()
        {
            DisableInteractions();
            await _fader.FadeIn(this.GetCancellationTokenOnDestroy());
            EnableInteractions();
        }

        public async UniTask Hide()
        {
            DisableInteractions();
            await _fader.FadeOut(this.GetCancellationTokenOnDestroy());
            EnableInteractions();
        }

        public void SetInOffScreenPosition() =>
            _fader.SetInOffScreenPosition();

        private void EnableInteractions()
        {
            if(_graphicRaycaster != null)
                _graphicRaycaster.enabled = true;
        }

        private void DisableInteractions()
        {
            if(_graphicRaycaster != null)
                _graphicRaycaster.enabled = false;
        }
    }
}