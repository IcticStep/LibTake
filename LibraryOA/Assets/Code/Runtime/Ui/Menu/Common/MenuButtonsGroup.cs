using Code.Runtime.Ui.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.Ui.Menu.Common
{
    internal sealed class MenuButtonsGroup : MonoBehaviour
    {
        [SerializeField]
        private GraphicRaycaster _graphicRaycaster;
        [SerializeField]
        private FastFlyingFading _fader;
        
        public void EnableInteractions() =>
            _graphicRaycaster.enabled = true;
        
        public void DisableInteractions() => 
            _graphicRaycaster.enabled = false;

        public UniTask Show() =>
            _fader.FadeIn(this.GetCancellationTokenOnDestroy());
        
        public UniTask Hide() =>
            _fader.FadeOut(this.GetCancellationTokenOnDestroy());

        public void SetInOffScreenPosition() =>
            _fader.SetInOffScreenPosition();
    }
}