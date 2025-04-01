using System;
using Code.Runtime.Data;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Logic.CameraControl;
using Code.Runtime.StaticData.CharacterSelection;
using JetBrains.Annotations;
using UnityEngine;
using AudioSettings = Code.Runtime.Data.Settings.AudioSettings;

namespace Code.Runtime.Infrastructure.Services.SaveLoad
{
    [UsedImplicitly]
    internal sealed class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        private const string AudioSettingsKey = "AudioSettings";
        private const string CharacterSelectionKey = "CharacterSelection";
        private const string CameraSettingsKey = "CameraSettings";

        private readonly IPersistantProgressService _progressService;
        private readonly ISaveLoadRegistry _saveLoadRegistry;

        public bool HasSavedProgress => PlayerPrefs.HasKey(ProgressKey);
        public event Action Updated;
        public event Action Saved;

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
            Saved?.Invoke();
        }

        public GameProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey).ToDeserialized<GameProgress>();

        public void SaveAudioSettings(AudioSettings audioSettings)
        {
            PlayerPrefs.SetString(AudioSettingsKey, audioSettings.ToJson());
            PlayerPrefs.Save();
        }

        public AudioSettings LoadAudioSettings() =>
            PlayerPrefs.GetString(AudioSettingsKey).ToDeserialized<AudioSettings>()
            ?? new AudioSettings();

        public CameraTypeId LoadCameraSettings() =>
            PlayerPrefs.HasKey(CameraSettingsKey)
                ? PlayerPrefs.GetString(CameraSettingsKey).ToDeserialized<CameraTypeId>()
                : CameraTypeId.Low;

        public void SaveCameraSettings(CameraTypeId settings)
        {
            PlayerPrefs.SetString(CameraSettingsKey, settings.ToJson());
            PlayerPrefs.Save();
        }

        public CharacterTypeId LoadCharacterSelected() =>
            PlayerPrefs.HasKey(CharacterSelectionKey)
                ? PlayerPrefs.GetString(CharacterSelectionKey).ToDeserialized<CharacterTypeId>()
                : CharacterTypeId.Man1;

        public void SaveCharacterSelected(CharacterTypeId characterSelected)
        {
            PlayerPrefs.SetString(CharacterSelectionKey, characterSelected.ToJson());
            PlayerPrefs.Save();
        }

        public void DeleteProgress()
        {
            Updated?.Invoke();
            PlayerPrefs.DeleteKey(ProgressKey);
            PlayerPrefs.Save();
        }
    }
}