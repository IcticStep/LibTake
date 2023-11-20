using System.Collections.Generic;
using System.Linq;
using Code.Runtime.StaticData;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.StaticData
{
    [UsedImplicitly]
    public sealed class StaticDataService : IStaticDataService
    {
        private const string BooksPath = "Static Data/Books/Instances";
        private const string LevelsPath = "Static Data/Levels";
        private const string ReadingTablePath = "Static Data/Interactables/ReadingTableData";
        private const string StartupSettingsPath = "Static Data/StartupSettings";
        private const string TruckPath = "Static Data/Interactables/Truck static data";

        private Dictionary<string, StaticBook> _books = new();
        private Dictionary<string, LevelStaticData> _levels = new();

        public StartupSettings StartupSettings { get; private set; }
        public StaticReadingTable ReadingTableData { get; private set; }
        public TruckStaticData TruckData { private set; get; }

        public void LoadAll()
        {
            LoadBooks();
            LoadLevels();
            LoadInteractables();
            LoadStartupSettings();
        }

        public void LoadBooks() =>
            _books = Resources
                .LoadAll<StaticBook>(BooksPath)
                .ToDictionary(x => x.Id, x => x);

        public void LoadLevels() =>
            _levels = Resources
                .LoadAll<LevelStaticData>(LevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);

        public void LoadInteractables()
        {
            ReadingTableData = Resources.Load<StaticReadingTable>(ReadingTablePath);
            TruckData = Resources.Load<TruckStaticData>(TruckPath);
        }

        public void LoadStartupSettings() =>
            StartupSettings = Resources
                .Load<StartupSettings>(StartupSettingsPath);

        public StaticBook ForBook(string id) =>
            _books.TryGetValue(id, out StaticBook result)
                ? result
                : null;

        public LevelStaticData ForLevel(string key) =>
            _levels.TryGetValue(key, out LevelStaticData result)
                ? result
                : null;
    }
}