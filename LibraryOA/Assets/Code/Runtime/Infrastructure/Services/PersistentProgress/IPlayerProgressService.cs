using Code.Runtime.Data;
using Code.Runtime.Data.Progress;

namespace Code.Runtime.Infrastructure.Services.PersistentProgress
{
    internal interface IPlayerProgressService
    {
        PlayerProgress Progress { get; set; }
    }
}