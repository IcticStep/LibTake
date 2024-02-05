using Code.Runtime.StaticData.Ui;
using Code.Runtime.Ui.Common;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Code.Runtime.Ui.Messages
{
    public sealed class DayMessage : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        [SerializeField]
        private TextMeshProUGUI _textMessage;
        [SerializeField]
        private LocalizeStringEvent[] _localizeStringEvents;

        private void Awake() =>
            _smoothFader.FadeImmediately();

        private void OnValidate() =>
            _localizeStringEvents = GetComponentsInChildren<LocalizeStringEvent>();
        
        public async UniTask Show()
        {
            foreach (LocalizeStringEvent localizeStringEvent in _localizeStringEvents)
                localizeStringEvent.RefreshString();
            await _smoothFader.UnFadeAsync();
        }

        public async UniTask Hide()
        {
            await _smoothFader.FadeAsync();
            _textMessage.text = string.Empty;
        }
        
        public void ConfigureTimings(UiMessageIntervals intervals) =>
            _smoothFader.Initialize(intervals.ShowAnimationTime, intervals.HideAnimationTime);
    }
}