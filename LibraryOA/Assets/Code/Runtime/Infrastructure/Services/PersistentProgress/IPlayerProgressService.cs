using Code.Runtime.Data;

namespace Code.Runtime.Infrastructure.Services.PersistentProgress
{
    internal interface IPlayerProgressService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}