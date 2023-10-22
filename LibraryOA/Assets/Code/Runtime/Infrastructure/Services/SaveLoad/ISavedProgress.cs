using Code.Runtime.Data;

namespace Code.Runtime.Infrastructure.Services.SaveLoad
{
    public interface ISavedProgress : ISavedProgressReader
    {
        public void UpdateProgress(PlayerProgress progress);
    }
}