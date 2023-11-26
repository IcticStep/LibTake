using Code.Runtime.Data;
using Code.Runtime.Data.Progress;
using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.Services.PersistentProgress
{
    [UsedImplicitly]
    internal sealed class PlayerProgressService : IPlayerProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}