using Code.Runtime.Data.Progress;

namespace Code.Runtime.Infrastructure.Services.PersistentProgress
{
    internal interface IPersistantProgressService
    {
        Progress Progress { get; set; }
    }
}