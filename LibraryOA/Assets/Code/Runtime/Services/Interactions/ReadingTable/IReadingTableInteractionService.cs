using Code.Runtime.Logic;
using Code.Runtime.Logic.Interactions.Data;

namespace Code.Runtime.Services.Interactions.ReadingTable
{
    internal interface IReadingTableInteractionService
    {
        bool CanInteract(IBookStorage bookStorage, IProgress progress);
        void Interact(IBookStorage bookStorage, IProgress progress);
        void StartReadingIfPossible(IBookStorage bookStorage, IProgress progress);
        void StopReading(IProgress progress);
    }
}