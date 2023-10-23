using Code.Runtime.Data;
using Code.Runtime.Data.Progress;

namespace Code.Runtime.Infrastructure.Services.PersistentProgress
{
    internal sealed class PlayerProgressService : IPlayerProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}