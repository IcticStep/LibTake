using System;
using Code.Runtime.StaticData;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class Skill
    {
        public BookType BookType;
        public int Level;

        public Skill(BookType bookType, int level)
        {
            BookType = bookType;
            Level = level;
        }

        public void AddLevels(int count)
        {
            if(count <= 0)
                return;

            Level += count;
        }
    }
}