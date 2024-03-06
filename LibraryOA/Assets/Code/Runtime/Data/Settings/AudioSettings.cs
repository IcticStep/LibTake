using System;

namespace Code.Runtime.Data.Settings
{
    [Serializable]
    public sealed class AudioSettings
    {
        public bool MusicEnabled = true;
        public bool SfxEnabled = true;
    }
}