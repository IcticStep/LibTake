using Code.Runtime.Data.Progress;

namespace Code.Runtime.Infrastructure.Services.SaveLoad
{
    internal interface ISaveLoadService
    {
        void SaveProgress();
        Progress LoadProgress();
        void DeleteProgress();
    }
}