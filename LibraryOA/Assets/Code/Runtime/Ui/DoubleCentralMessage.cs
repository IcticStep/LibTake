
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.Ui
{
    internal sealed class DoubleCentralMessage : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        [SerializeField]
        private TextMeshProUGUI _header;
        [SerializeField]
        private TextMeshProUGUI _subHeader;

        private void Awake() =>
            _smoothFader.FadeImmediately();

        public async UniTask Show(string header, string subHeader)
        {
            _header.text = header;
            _subHeader.text = subHeader;
            await _smoothFader.UnFadeAsync();
        }
        
        public async UniTask Hide()
        {
            _header.text = string.Empty;
            _subHeader.text = string.Empty;
            await _smoothFader.FadeAsync();
        }
    }
}