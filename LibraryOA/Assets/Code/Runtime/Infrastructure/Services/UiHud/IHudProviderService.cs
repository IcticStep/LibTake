using Code.Runtime.Ui.Messages;

namespace Code.Runtime.Infrastructure.Services.UiHud
{
    internal interface IHudProviderService
    {
        Ui.HudComponents.Hud Hud { get; }
        DayMessage DayMessage { get; }
        MorningMessage MorningMessage { get; }
        void CleanUp();
        void Show();
        void Hide();
        void RegisterHud(Ui.HudComponents.Hud hud);
    }
}