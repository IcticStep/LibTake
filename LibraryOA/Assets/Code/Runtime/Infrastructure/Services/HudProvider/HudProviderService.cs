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
        public CentralMessage CentralMessage { get; private set; }
        public DoubleCentralMessage DoubleCentralMessage { get; private set; }

        public void RegisterHud(GameObject hud)
        {
            Hud = hud;
            CentralMessage = Hud.GetComponentInChildren<CentralMessage>();
            DoubleCentralMessage = Hud.GetComponentInChildren<DoubleCentralMessage>();
        }

        public void CleanUp()
        {
            Hud = null;
            CentralMessage = null;
            DoubleCentralMessage = null;
        }
    }
}