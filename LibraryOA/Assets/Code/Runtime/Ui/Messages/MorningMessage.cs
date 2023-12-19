using Code.Runtime.StaticData.Ui;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Code.Runtime.Ui.Messages
{
    internal sealed class MorningMessage : MonoBehaviour
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
        
        public void ConfigureTimings(UiMessageIntervals intervals) =>
            _smoothFader.Configure(intervals.ShowAnimationTime, intervals.HideAnimationTime);
    }
}