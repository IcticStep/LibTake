using Code.Runtime.StaticData;

namespace Code.Runtime.Infrastructure.Services.StaticData
{
    internal interface IStaticDataService
    {
        StaticReadingTable ReadingTableData { get; }
        void LoadAll();
        void LoadBooks();
        void LoadLevels();
        void LoadInteractables();
        StaticBook ForBook(string id);
        LevelStaticData ForLevel(string key);
    }
}