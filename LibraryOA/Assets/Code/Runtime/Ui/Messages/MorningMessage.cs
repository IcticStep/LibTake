using System;
using Code.Runtime.StaticData.Ui;
using Code.Runtime.Ui.Common;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

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
            _header.text = string.Empty;
            _subHeader.text = string.Empty;
        }
        
        public void ConfigureTimings(UiMessageIntervals intervals) =>
            _smoothFader.Initialize(intervals.ShowAnimationTime, intervals.HideAnimationTime);
    }
}