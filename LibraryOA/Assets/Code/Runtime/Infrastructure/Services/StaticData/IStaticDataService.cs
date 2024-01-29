using System.Collections.Generic;
using Code.Runtime.StaticData;
using Code.Runtime.StaticData.Balance;
using Code.Runtime.StaticData.Books;
using Code.Runtime.StaticData.GlobalGoals;
using Code.Runtime.StaticData.Interactables;
using Code.Runtime.StaticData.Level;
using Code.Runtime.StaticData.Ui;

namespace Code.Runtime.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        ScenesRouting ScenesRouting { get; }
        InteractablesStaticData Interactables { get; }
        StaticPlayer Player { get; }
        StaticBookReceiving BookReceiving { get; }
        StaticBooksDelivering BookDelivering { get; }
        UiData Ui { get; }
        StaticLevelStartingSettings LevelStartSettings { get; }
        IReadOnlyList<StaticBook> AllBooks { get; }
        IReadOnlyList<StaticBookType> BookTypes { get; }
        IReadOnlyList<GlobalGoal> GlobalGoals { get; }
        LevelStaticData CurrentLevelData { get; }
        void LoadAll();
        void LoadStartupSettings();
        void LoadLevelStartSettings();
        void LoadBooks();
        void LoadLevels();
        void LoadPlayer();
        void LoadBookTypes();
        void LoadGlobalGoals();
        void LoadBookReceiving();
        void LoadBookDelivering();
        void LoadUi();
        void LoadInteractables();
        StaticBook ForBook(string id);
        LevelStaticData ForLevel(string key);
    }
}