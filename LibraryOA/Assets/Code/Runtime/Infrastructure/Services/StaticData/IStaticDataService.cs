using Code.Runtime.StaticData;

namespace Code.Runtime.Infrastructure.Services.StaticData
{
    internal interface IStaticDataService
    {
        void LoadAll();
        void LoadBooks();
        void LoadLevels();
        StaticBook ForBook(string id);
        LevelStaticData ForLevel(string key);
    }
}