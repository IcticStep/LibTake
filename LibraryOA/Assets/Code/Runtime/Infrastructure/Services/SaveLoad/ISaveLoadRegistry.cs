using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadRegistry
    {
        IReadOnlyList<ISavedProgressReader> ProgressReaders { get; }
        IReadOnlyList<ISavedProgress> ProgressWriters { get; }
        void RegisterAllComponents(GameObject gameObject);
        void UnregisterAllComponents(GameObject gameObject);
        void Unregister(ISavedProgressReader progressReader);
        void Register(ISavedProgressReader progressReader);
    }
}