using System;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates;

namespace Code.Runtime.Logic.Interactables.Crafting.Ui.StatesCanvases.Pay
{
    internal sealed class PayStateCanvasView : CanvasViewForState<PayState>
    {
        public event Action<int> CoinPriceUpdated;

        protected override void OnCanvasShow(PayState state) =>
            CoinPriceUpdated?.Invoke(state.Price);
    }
}