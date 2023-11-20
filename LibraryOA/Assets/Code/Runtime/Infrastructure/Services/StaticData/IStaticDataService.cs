using Code.Runtime.StaticData;

namespace Code.Runtime.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        StartupSettings StartupSettings { get; }
        StaticReadingTable ReadingTableData { get; }
        TruckStaticData TruckData { get; }
        void LoadAll();
        void LoadBooks();
        void LoadLevels();
        void LoadInteractables();
        void LoadStartupSettings();
        StaticBook ForBook(string id);
        LevelStaticData ForLevel(string key);
    }
}