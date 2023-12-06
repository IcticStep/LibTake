using System;
using Code.Runtime.Infrastructure.Services.SaveLoad;

namespace Code.Runtime.Services.Player.Lives
{
    internal interface IPlayerLivesService : ISavedProgress
    {
        void WasteLife();
        void RestoreLife();
        int Health { get; }
        event Action Updated;
    }
}