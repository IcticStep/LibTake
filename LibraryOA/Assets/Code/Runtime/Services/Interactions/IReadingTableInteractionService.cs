using Code.Runtime.Data;

namespace Code.Runtime.Services.Interactions
{
    internal interface IReadingTableInteractionService
    {
        bool CanInteract(IBookStorage bookStorage);
        void Interact(IBookStorage bookStorage);
    }
}