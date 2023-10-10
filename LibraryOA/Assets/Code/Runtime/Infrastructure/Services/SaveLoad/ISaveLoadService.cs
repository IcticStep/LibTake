using Code.Runtime.Data;

namespace Code.Runtime.Infrastructure.Services.SaveLoad
{
    internal interface ISaveLoadService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}