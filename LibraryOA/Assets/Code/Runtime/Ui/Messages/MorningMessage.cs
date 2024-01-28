using Code.Runtime.StaticData.Ui;
using Code.Runtime.Ui.Common;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Code.Runtime.Ui.Messages
{
    public sealed class MorningMessage : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        [SerializeField]
        private TextMeshProUGUI _header;
        [SerializeField]
        private TextMeshProUGUI _subHeader;
        
        private void Awake() =>
            _smoothFader.FadeImmediately();

        public async UniTask Show() =>
            await _smoothFader.UnFadeAsync();

        public async UniTask Hide()
        {
            await _smoothFader.FadeAsync();
            _header.text = string.Empty;
            _subHeader.text = string.Empty;
        }
        
        public void ConfigureTimings(UiMessageIntervals intervals) =>
            _smoothFader.Initialize(intervals.ShowAnimationTime, intervals.HideAnimationTime);
    }
}