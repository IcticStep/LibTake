using System.Collections.Generic;

namespace Code.Runtime.Infrastructure.Services.SaveLoad
{
    internal interface ISaveLoadRegistry
    {
        IReadOnlyList<ISavedProgressReader> ProgressReaders { get; }
        IReadOnlyList<ISavedProgress> ProgressWriters { get; }
        void Unregister(ISavedProgressReader progressReader);
        void Register(ISavedProgressReader progressReader);
    }
}