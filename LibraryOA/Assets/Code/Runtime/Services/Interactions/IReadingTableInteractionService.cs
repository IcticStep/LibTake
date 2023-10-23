using Code.Runtime.Data;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Interactions.Data;

namespace Code.Runtime.Services.Interactions
{
    internal interface IReadingTableInteractionService
    {
        bool CanInteract(IBookStorage bookStorage, Progress progress);
        void Interact(IBookStorage bookStorage, Progress progress);
        void StartReadingIfPossible(IBookStorage bookStorage, Progress progress);
        void StopReading(Progress progress);
    }
}