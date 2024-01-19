using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.StaticData.Ui;
using Code.Runtime.Ui.Common;
using Code.Runtime.Ui.Messages;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.HudComponents
{
    public class Hud : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _hudFader;

        private IStaticDataService _staticDataService;

        [field: SerializeField]
        public DayMessage DayMessage { get; private set; }

        [field: SerializeField]
        public MorningMessage MorningMessage { get; private set; }

        private HudData HudData => _staticDataService.Ui.Hud;

        [Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _hudFader.Initialize(
                HudData.ShowHideAnimationSeconds,
                HudData.ShowHideAnimationSeconds,
                HudData.ShowHideAnimationEase,
                HudData.ShowHideAnimationEase);
        }

        public void Show() =>
            _hudFader.UnFade();

        public void Hide() =>
            _hudFader.Fade();
    }
}