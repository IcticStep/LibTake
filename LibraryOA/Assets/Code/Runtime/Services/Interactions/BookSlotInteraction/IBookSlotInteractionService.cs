using Code.Runtime.Logic.Interactables.Data;

namespace Code.Runtime.Services.Interactions.BookSlotInteraction
{
    internal interface IBookSlotInteractionService
    {
        bool CanInteract(IBookStorage bookStorage);
        void Interact(IBookStorage bookStorage);
    }
}