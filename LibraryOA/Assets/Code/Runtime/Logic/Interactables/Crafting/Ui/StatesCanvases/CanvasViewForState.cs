using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using UnityEngine;

namespace Code.Runtime.Logic.Interactables.Crafting.Ui.StatesCanvases
{
    internal abstract class CanvasViewForState<TState> : MonoBehaviour
        where TState : ICraftingTableState
    {
        [SerializeField]
        private Canvas _canvas;
        [SerializeField]
        private CraftingTableStateMachine _craftingTableStateMachine;

        private void Awake()
        {
            HideCanvas();
            
            _craftingTableStateMachine.EnterState += OnStateEnter;
            _craftingTableStateMachine.ExitState += OnStateExit;
        }

        private void OnDestroy()
        {
            _craftingTableStateMachine.EnterState -= OnStateEnter;
            _craftingTableStateMachine.ExitState -= OnStateExit;
        }

        private void OnStateEnter(ICraftingTableState state)
        {
            if(state is not TState castedState)
                return;
            
            OnCanvasShow(castedState);
            ShowCanvas();
        }

        private void OnStateExit(ICraftingTableState state)
        {
            if(state is not TState castedState)
                return;

            HideCanvas();
            AfterCanvasHide(castedState);
        }
        
        protected virtual void OnCanvasShow(TState state) { }
        protected virtual void AfterCanvasHide(TState state) { }

        private void HideCanvas() =>
            _canvas.enabled = false;

        private void ShowCanvas() =>
            _canvas.enabled = true;
    }
}