using System.Threading;

namespace Code.Runtime.Infrastructure.Services.CleanUp
{
    internal interface ILevelCleanUpService
    {
        void CleanUp();
        CancellationToken RestartCancellationToken { get; }
    }
}