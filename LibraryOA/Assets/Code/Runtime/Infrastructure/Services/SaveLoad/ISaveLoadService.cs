using System;
using Code.Runtime.Data.Progress;
using Code.Runtime.Data.Settings;
using Code.Runtime.Logic.CameraControl;
using Code.Runtime.StaticData.CharacterSelection;

namespace Code.Runtime.Infrastructure.Services.SaveLoad
{
    internal interface ISaveLoadService
    {
        bool HasSavedProgress { get; }
        event Action Updated;
        event Action Saved;
        void SaveProgress();
        GameProgress LoadProgress();
        void SaveAudioSettings(AudioSettings audioSettings);
        AudioSettings LoadAudioSettings();
        CameraTypeId LoadCameraSettings();
        void SaveCameraSettings(CameraTypeId settings);
        CharacterTypeId LoadCharacterSelected();
        void SaveCharacterSelected(CharacterTypeId characterSelected);
        void DeleteProgress();
    }
}