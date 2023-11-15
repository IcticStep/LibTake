using Code.Runtime.StaticData;

namespace Code.Runtime.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        StaticReadingTable ReadingTableData { get; }
        StartupSettings StartupSettings { get; }
        void LoadAll();
        void LoadBooks();
        void LoadLevels();
        void LoadInteractables();
        void LoadStartupSettings();
        StaticBook ForBook(string id);
        LevelStaticData ForLevel(string key);
    }
}