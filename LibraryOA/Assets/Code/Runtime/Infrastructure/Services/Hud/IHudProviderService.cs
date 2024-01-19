using Code.Runtime.Ui.Messages;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Hud
{
    internal interface IHudProviderService
    {
        GameObject Hud { get; }
        DayMessage DayMessage { get; }
        MorningMessage MorningMessage { get; }
        void RegisterHud(GameObject hud, Canvas mainCanvas);
        void CleanUp();
        void Show();
        void Hide();
    }
}