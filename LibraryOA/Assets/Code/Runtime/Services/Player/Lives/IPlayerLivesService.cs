using System;
using Code.Runtime.Infrastructure.Services.SaveLoad;

namespace Code.Runtime.Services.Player.Lives
{
    public interface IPlayerLivesService : ISavedProgress
    {
        void WasteLife();
        void RestoreLife();
        int Lives { get; }
        event Action Updated;
    }
}