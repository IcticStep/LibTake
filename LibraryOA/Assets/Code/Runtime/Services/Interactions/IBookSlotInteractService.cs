using Code.Runtime.Logic;

namespace Code.Runtime.Services.Interactions
{
    internal interface IBookSlotInteractService
    {
        bool CanInteract(BookStorage bookStorage);
        void Interact(BookStorage bookStorage);
    }
}