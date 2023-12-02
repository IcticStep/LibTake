using System.Collections.Generic;
using Code.Runtime.StaticData;
using Code.Runtime.StaticData.Balance;
using Code.Runtime.StaticData.Books;
using Code.Runtime.StaticData.Interactables;
using Code.Runtime.StaticData.Level;

namespace Code.Runtime.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        StartupSettings StartupSettings { get; }
        InteractablesStaticData Interactables { get; }
        StaticPlayer Player { get; }
        StaticBookReceiving BookReceiving { get; }
        IReadOnlyList<StaticBook> AllBooks { get; }
        void LoadAll();
        void LoadBooks();
        void LoadLevels();
        void LoadPlayer();
        void LoadBookReceiving();
        void LoadInteractables();
        void LoadStartupSettings();
        StaticBook ForBook(string id);
        LevelStaticData ForLevel(string key);
    }
}