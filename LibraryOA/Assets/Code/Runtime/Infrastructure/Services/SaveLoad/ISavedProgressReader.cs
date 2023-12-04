using Code.Runtime.Data;
using Code.Runtime.Data.Progress;

namespace Code.Runtime.Infrastructure.Services.SaveLoad
{
    public interface ISavedProgressReader
    {
        public void LoadProgress(Progress progress);
    }
}