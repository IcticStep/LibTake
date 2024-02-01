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
        
        public void EnableInteractions() =>
            _graphicRaycaster.enabled = true;
        
        public void DisableInteractions() => 
            _graphicRaycaster.enabled = false;

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
        }

        public void SetInOffScreenPosition() =>
            _fader.SetInOffScreenPosition();
    }
}