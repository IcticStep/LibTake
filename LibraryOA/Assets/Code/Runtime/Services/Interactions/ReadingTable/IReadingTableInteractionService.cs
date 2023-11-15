using Code.Runtime.Logic;
using Code.Runtime.Logic.Interactions.Data;

namespace Code.Runtime.Services.Interactions.ReadingTable
{
    internal interface IReadingTableInteractionService
    {
        bool CanInteract(IBookStorage bookStorage, Progress progress);
        void Interact(IBookStorage bookStorage, Progress progress);
        void StartReadingIfPossible(IBookStorage bookStorage, Progress progress);
        void StopReading(Progress progress);
    }
}