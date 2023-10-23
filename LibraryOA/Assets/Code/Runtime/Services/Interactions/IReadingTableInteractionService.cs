using Code.Runtime.Data;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Interactions.Data;

namespace Code.Runtime.Services.Interactions
{
    internal interface IReadingTableInteractionService
    {
        bool CanInteract(IBookStorage bookStorage);
        void Interact(IBookStorage bookStorage, Progress progress);
    }
}