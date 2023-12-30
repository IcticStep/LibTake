using Code.Runtime.StaticData.Ui;
using Code.Runtime.Ui.Behaviours;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Code.Runtime.Ui.Messages
{
    internal sealed class DayMessage : MonoBehaviour
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
            await _smoothFader.FadeAsync();
            _textMessage.text = string.Empty;
        }
        
        public void ConfigureTimings(UiMessageIntervals intervals) =>
            _smoothFader.Configure(intervals.ShowAnimationTime, intervals.HideAnimationTime);
    }
}