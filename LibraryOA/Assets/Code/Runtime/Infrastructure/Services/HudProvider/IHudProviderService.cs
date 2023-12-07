using Code.Runtime.Ui;
using Code.Runtime.Ui.Messages;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.HudProvider
{
    internal interface IHudProviderService
    {
        GameObject Hud { get; }
        CentralMessage CentralMessage { get; }
        DoubleCentralMessage DoubleCentralMessage { get; }
        void RegisterHud(GameObject hud);
        void CleanUp();
    }
}