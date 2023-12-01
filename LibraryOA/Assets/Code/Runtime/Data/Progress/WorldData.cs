using System;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class WorldData
    {
        public BookHoldersState BookHoldersState = new();
        public ProgressesStates ProgressesStates = new();
        public BooksDeliveringData BooksDeliveringData = new();
        public TimeData TimeData = new();
    }
}