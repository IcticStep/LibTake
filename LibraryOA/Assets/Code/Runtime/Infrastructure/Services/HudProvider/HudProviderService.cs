using Code.Runtime.Ui;
using Code.Runtime.Ui.Messages;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.HudProvider
{
    [UsedImplicitly]
    internal sealed class HudProviderService : IHudProviderService
    {
        public GameObject Hud { get; private set; }
        public DayMessage DayMessage { get; private set; }
        public MorningMessage MorningMessage { get; private set; }

        public void RegisterHud(GameObject hud)
        {
            Hud = hud;
            DayMessage = Hud.GetComponentInChildren<DayMessage>();
            MorningMessage = Hud.GetComponentInChildren<MorningMessage>();
        }

        public void CleanUp()
        {
            Hud = null;
            DayMessage = null;
            MorningMessage = null;
        }
    }
}