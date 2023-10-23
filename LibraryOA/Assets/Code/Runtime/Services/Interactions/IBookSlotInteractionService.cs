using Code.Runtime.Data;

namespace Code.Runtime.Services.Interactions
{
    internal interface IBookSlotInteractionService
    {
        bool CanInteract(IBookStorage bookStorage);
        void Interact(IBookStorage bookStorage);
    }
}