using System;
using Code.Runtime.Data;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.SaveLoad
{
    [UsedImplicitly]
    internal sealed class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        private readonly IPersistantProgressService _progressService;
        private readonly ISaveLoadRegistry _saveLoadRegistry;

        public bool HasSavedProgress => PlayerPrefs.HasKey(ProgressKey);
        public event Action Updated;

        public SaveLoadService(IPersistantProgressService progressService, ISaveLoadRegistry saveLoadRegistry)
        {
            _progressService = progressService;
            _saveLoadRegistry = saveLoadRegistry;
        }

        public void SaveProgress()
        {
            foreach(ISavedProgress progressWriter in _saveLoadRegistry.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);
            
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
            Updated?.Invoke();
        }

        public GameProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey).ToDeserialized<GameProgress>();

        public void DeleteProgress()
        {
            Updated?.Invoke();
            PlayerPrefs.DeleteKey(ProgressKey);
            PlayerPrefs.Save();
        }
    }
}