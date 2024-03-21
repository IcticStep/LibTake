using Code.Runtime.Ui.Messages;
using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.Services.UiHud
{
    [UsedImplicitly]
    internal sealed class HudService : IHudProviderService
    {
        public Ui.HudComponents.Hud Hud { get; private set; }
        public DayMessage DayMessage { get; private set; }
        public MorningMessage MorningMessage { get; private set; }
        
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