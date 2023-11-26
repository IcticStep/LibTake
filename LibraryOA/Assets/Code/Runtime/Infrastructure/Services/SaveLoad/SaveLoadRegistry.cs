using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.SaveLoad
{
    [UsedImplicitly]
    public sealed class SaveLoadRegistry : ISaveLoadRegistry
    {
        private readonly List<ISavedProgressReader> _progressReaders = new();
        private readonly List<ISavedProgress> _progressWriters = new();

        public IReadOnlyList<ISavedProgressReader> ProgressReaders => _progressReaders;
        public IReadOnlyList<ISavedProgress> ProgressWriters => _progressWriters;

        public void RegisterAllComponents(GameObject gameObject)
        {
            ISavedProgressReader[] progressReaders = gameObject.GetComponentsInChildren<ISavedProgressReader>();
            foreach(ISavedProgressReader progressReader in progressReaders)
                Register(progressReader);
        }

        public void UnregisterAllComponents(GameObject gameObject)
        {
            ISavedProgressReader[] progressReaders = gameObject.GetComponentsInChildren<ISavedProgressReader>();
            foreach(ISavedProgressReader progressReader in progressReaders)
                Unregister(progressReader);
        }

        public void Unregister(ISavedProgressReader progressReader)
        {
            _progressReaders.Remove(progressReader);

            if(progressReader is ISavedProgress progressWriter)
                _progressWriters.Remove(progressWriter);
        }

        public void Register(ISavedProgressReader progressReader)
        {
            _progressReaders.Add(progressReader);

            if(progressReader is ISavedProgress progressWriter)
                _progressWriters.Add(progressWriter);
        }
    }
}