using Code.Runtime.Data;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.SaveLoad
{
    internal sealed class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        private readonly IPlayerProgressService _progressService;

        public SaveLoadService(IPlayerProgressService progressService)
        {
            _progressService = progressService;
        }

        public void SaveProgress() =>
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey).ToDeserialized<PlayerProgress>();
    }
}