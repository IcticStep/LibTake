using Code.Runtime.Data;

namespace Code.Runtime.Services.Interactions
{
    internal interface IBookSlotInteractService
    {
        bool CanInteract(IBookStorage bookStorage);
        void Interact(IBookStorage bookStorage);
    }
}