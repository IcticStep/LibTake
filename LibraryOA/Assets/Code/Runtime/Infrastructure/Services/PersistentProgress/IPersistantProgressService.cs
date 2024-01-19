using Code.Runtime.Data.Progress;

namespace Code.Runtime.Infrastructure.Services.PersistentProgress
{
    internal interface IPersistantProgressService
    {
        GameProgress Progress { get; set; }
    }
}