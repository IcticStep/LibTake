using Code.Runtime.Data.Progress;

namespace Code.Runtime.Infrastructure.Services.SaveLoad
{
    public interface ISavedProgress : ISavedProgressReader
    {
        public void UpdateProgress(Progress progress);
    }
}