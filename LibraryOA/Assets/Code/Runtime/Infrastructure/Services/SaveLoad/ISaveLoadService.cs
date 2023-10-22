using Code.Runtime.Data;
using Code.Runtime.Data.Progress;

namespace Code.Runtime.Infrastructure.Services.SaveLoad
{
    internal interface ISaveLoadService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}