using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Code.Runtime.Ui
{
    internal sealed class CentralMessage : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        [SerializeField]
        private TextMeshProUGUI _textMessage;

        private void Awake() =>
            _smoothFader.FadeImmediately();

        public async UniTask Show(string text)
        {
            _textMessage.text = text;
            await _smoothFader.UnFadeAsync();
        }
        
        public async UniTask Hide()
        {
            _textMessage.text = string.Empty;
            await _smoothFader.FadeAsync();
        }
    }
}