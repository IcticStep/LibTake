using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class PlayerProgress
    {
        public BookData PlayerInventory = new();
        public SkillStats SkillStats = new();
        public WorldData WorldData;
    }

    [Serializable]
    public sealed class WorldData
    {
        private List<BookIdentifiedHolderData> _bookHolders = new();
        
        public BookData GetDataForId(string id) =>
            _bookHolders.FirstOrDefault(x => x.Id == id)?.BookData;

        public void SetDataForId(string id, BookData data)
        {
            BookIdentifiedHolderData savedData = _bookHolders.FirstOrDefault(x => x.Id == id);

            if(savedData is not null)
            {
                savedData.BookData = data;
                return;
            }
            
            _bookHolders.Add(new BookIdentifiedHolderData(id, data));
        }
    }
}