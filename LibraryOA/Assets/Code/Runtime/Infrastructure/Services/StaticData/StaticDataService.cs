using System.Collections.Generic;
using System.Linq;
using Code.Runtime.StaticData;
using Code.Runtime.StaticData.Balance;
using Code.Runtime.StaticData.Books;
using Code.Runtime.StaticData.GlobalGoals;
using Code.Runtime.StaticData.Interactables;
using Code.Runtime.StaticData.Level;
using Code.Runtime.StaticData.Ui;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Runtime.Infrastructure.Services.StaticData
{
    [UsedImplicitly]
    public sealed class StaticDataService : IStaticDataService
    {
        private const string BooksPath = "Static Data/Books/Instances";
        private const string LevelsPath = "Static Data/Levels";
        private const string ReadingTablePath = "Static Data/Interactables/ReadingTableData";
        private const string ScenesRoutingPath = "Static Data/Scenes Routing";
        private const string TruckPath = "Static Data/Interactables/Truck static data";
        private const string BookSlotPath = "Static Data/Interactables/BookSlotData";
        private const string PlayerPath = "Static Data/Player";
        private const string BookReceivingPath = "Static Data/Book Receiving";
        private const string UiPath = "Static Data/Ui Data";
        private const string ScannerPath = "Static Data/Interactables/ScannerData";
        private const string StatuePath = "Static Data/Interactables/StatueData";
        private const string BookDeliveringPath = "Static Data/Books delivering";
        private const string CraftingTablePath = "Static Data/Interactables/Crafting Table Data";
        private const string BookTypesPath = "Static Data/Books/Types/";
        private const string GlobalGoalsPath = "Static Data/Global Goals/";
        private const string LevelStartPath = "Static Data/Level starting settings";

        private Dictionary<string, StaticBook> _books = new();
        private Dictionary<string, LevelStaticData> _levels = new();
        private List<StaticBookType> _bookTypes = new();
        private List<GlobalGoal> _globalGoals = new();

        public ScenesRouting ScenesRouting { get; private set; }
        public LevelStaticData CurrentLevelData => ForLevel(SceneManager.GetActiveScene().name);
        public StaticLevelStartingSettings LevelStartSettings { get; private set; }
        public StaticPlayer Player { get; private set; }
        public StaticBookReceiving BookReceiving { get; private set; }
        public StaticBooksDelivering BookDelivering { get; private set; }
        public IReadOnlyList<StaticBook> AllBooks => _books.Values.ToList();
        public IReadOnlyList<StaticBookType> BookTypes => _bookTypes;
        public InteractablesStaticData Interactables { get; private set; }
        public IReadOnlyList<GlobalGoal> GlobalGoals => _globalGoals;
        public UiData Ui { get; private set; }

        public void LoadAll()
        {
            LoadStartupSettings();
            LoadLevelStartSettings();
            LoadLevels();
            LoadPlayer();
            LoadBookReceiving();
            LoadBookDelivering();
            LoadBooks();
            LoadBookTypes();
            LoadInteractables();
            LoadGlobalGoals();
            LoadUi();
        }

        public void LoadStartupSettings() =>
            ScenesRouting = Resources
                .Load<ScenesRouting>(ScenesRoutingPath);

        public void LoadLevelStartSettings() =>
            LevelStartSettings = Resources
                .Load<StaticLevelStartingSettings>(LevelStartPath);

        public void LoadLevels() =>
            _levels = Resources
                .LoadAll<LevelStaticData>(LevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);

        public void LoadPlayer() =>
            Player = Resources
                .Load<StaticPlayer>(PlayerPath);

        public void LoadBookReceiving() =>
            BookReceiving = Resources
                .Load<StaticBookReceiving>(BookReceivingPath);

        public void LoadBookDelivering() =>
            BookDelivering = Resources
                .Load<StaticBooksDelivering>(BookDeliveringPath);

        public void LoadBooks() =>
            _books = Resources
                .LoadAll<StaticBook>(BooksPath)
                .ToDictionary(x => x.Id, x => x);

        public void LoadBookTypes() =>
            _bookTypes = Resources
                .LoadAll<StaticBookType>(BookTypesPath)
                .ToList();

        public void LoadInteractables()
        {
            StaticReadingTable readingTable = Resources.Load<StaticReadingTable>(ReadingTablePath);
            StaticBookSlot bookSlot = Resources.Load<StaticBookSlot>(BookSlotPath);
            StaticTruck truck = Resources.Load<StaticTruck>(TruckPath);
            StaticScanner scanner = Resources.Load<StaticScanner>(ScannerPath);
            StaticStatue statue = Resources.Load<StaticStatue>(StatuePath);
            StaticCraftingTable craftingTable = Resources.Load<StaticCraftingTable>(CraftingTablePath);
            
            Interactables = new InteractablesStaticData(readingTable, bookSlot, truck, scanner, statue, craftingTable);
        }

        public void LoadGlobalGoals() =>
            _globalGoals = Resources
                .LoadAll<GlobalGoal>(GlobalGoalsPath)
                .ToList();

        public void LoadUi() =>
            Ui = Resources
                .Load<UiData>(UiPath);

        public StaticBook ForBook(string id) =>
            _books.GetValueOrDefault(id);
        
        public LevelStaticData ForLevel(string key) =>
            _levels.GetValueOrDefault(key);
    }
}