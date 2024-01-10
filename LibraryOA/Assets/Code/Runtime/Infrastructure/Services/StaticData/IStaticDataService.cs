using System.Collections.Generic;
using Code.Runtime.StaticData;
using Code.Runtime.StaticData.Balance;
using Code.Runtime.StaticData.Books;
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
        UiData Ui { get; }
        IReadOnlyList<StaticBook> AllBooks { get; }
        LevelStaticData CurrentLevelData { get; }
        StaticBooksDelivering BookDelivering { get; }
        void LoadAll();
        void LoadBooks();
        void LoadLevels();
        void LoadPlayer();
        void LoadBookReceiving();
        void LoadUi();
        void LoadInteractables();
        void LoadStartupSettings();
        StaticBook ForBook(string id);
        LevelStaticData ForLevel(string key);
        void LoadBookDelivering();
    }
}