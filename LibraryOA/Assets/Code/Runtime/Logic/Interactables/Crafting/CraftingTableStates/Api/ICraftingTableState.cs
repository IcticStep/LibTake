using Cysharp.Threading.Tasks;

namespace Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api
{
    public interface ICraftingTableState
    {
        public bool CanInteract();
        public void Interact();
    }
}