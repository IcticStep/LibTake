using System.Collections.Generic;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Logic.Interactions.Data;

namespace Code.Runtime.Services.Player
{
    internal interface IPlayerInventoryService : IBookStorage, ISavedProgressReader
    {
        IReadOnlyList<string> Books { get; }
        void InsertBooks(IEnumerable<string> bookIds);
    }
}