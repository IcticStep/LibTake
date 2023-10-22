using Code.Runtime.Data;

namespace Code.Runtime.Infrastructure.Services.SaveLoad
{
    public interface ISavedProgressReader
    {
        public void LoadProgress(PlayerProgress progress);
    }
}