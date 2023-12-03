using Code.Runtime.Ui;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.HudProvider
{
    internal interface IHudProviderService
    {
        GameObject Hud { get; }
        CentralMessage CentralMessage { get; }
        void RegisterHud(GameObject hud);
        void CleanUp();
    }
}