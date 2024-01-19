using Code.Runtime.Ui.Messages;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.UiHud
{
    [UsedImplicitly]
    internal sealed class HudService : IHudProviderService
    {
        private Canvas _mainCanvas;
        public Ui.HudComponents.Hud Hud { get; private set; }
        public DayMessage DayMessage { get; private set; }
        public MorningMessage MorningMessage { get; private set; }

        private Tweener _currentTweener;

        public void RegisterHud(Ui.HudComponents.Hud hud)
        {
            Hud = hud;
            DayMessage = Hud.DayMessage;
            MorningMessage = Hud.MorningMessage;
        }

        public void Show() =>
            Hud.Show();

        public void Hide() =>
            Hud.Hide();

        public void CleanUp()
        {
            Hud = null;
            DayMessage = null;
            MorningMessage = null;
        }
    }
}