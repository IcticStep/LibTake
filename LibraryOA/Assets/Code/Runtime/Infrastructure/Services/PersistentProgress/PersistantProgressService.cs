using Code.Runtime.Data.Progress;
using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.Services.PersistentProgress
{
    [UsedImplicitly]
    internal sealed class PersistantProgressService : IPersistantProgressService
    {
        public Progress Progress { get; set; }
    }
}