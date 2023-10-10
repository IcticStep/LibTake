using Code.Runtime.Data;

namespace Code.Runtime.Infrastructure.Services.PersistentProgress
{
    internal sealed class PlayerProgressService : IPlayerProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}