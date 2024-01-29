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
        LevelStaticData CurrentLevelData { get; }
        StaticLevelStartingSettings LevelStartSettings { get; }
        StaticPlayer Player { get; }
        StaticBookReceiving BookReceiving { get; }
        StaticBooksDelivering BookDelivering { get; }
        IReadOnlyList<StaticBook> AllBooks { get; }
        IReadOnlyList<StaticBookType> BookTypes { get; }
        InteractablesStaticData Interactables { get; }
        IReadOnlyList<GlobalGoal> GlobalGoals { get; }
        UiData Ui { get; }
        void LoadAll();
        void LoadStartupSettings();
        void LoadLevelStartSettings();
        void LoadLevels();
        void LoadPlayer();
        void LoadBookReceiving();
        void LoadBookDelivering();
        void LoadBooks();
        void LoadBookTypes();
        void LoadInteractables();
        void LoadGlobalGoals();
        void LoadUi();
        StaticBook ForBook(string id);
        LevelStaticData ForLevel(string key);
    }
}