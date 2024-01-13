using System;
using System.Collections.Generic;
using Code.Runtime.Logic.Interactables.Api;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.Services.Interactions.Crafting;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Crafting
{
    public sealed class CraftingTableStateMachine : Interactable, IHoverStartListener, IHoverEndListener
    {
        [SerializeField]
        private Progress _progress;

        private ICraftingTableState _activeState;
        private Dictionary<Type, ICraftingTableState> _states;
        private ICraftingService _craftingService;

        public bool InProgress => _progress.Running;
        public string ActiveStateName => _activeState is null ? "none" : _activeState.ToString();

        public event Action<ICraftingTableState> EnterState;
        public event Action<ICraftingTableState> ExitState;

        [Inject]
        public void Construct(ICraftingService craftingService) =>
            _craftingService = craftingService;

        private void Awake() =>
            _states = new Dictionary<Type, ICraftingTableState>()
            {
                [typeof(PayState)] = new PayState(this, _craftingService),
                [typeof(SkillCheckState)] = new SkillCheckState(this, _craftingService),
                [typeof(CraftingState)] = new CraftingState(this, _craftingService, _progress),
                [typeof(FinishCraftState)] = new FinishCraftState(this),
            };

        private void Start() =>
            Enter<PayState>();

        public override bool CanInteract() =>
            _activeState.CanInteract();

        public override void Interact()
        {
            if(!CanInteract())
                return;

            _activeState.Interact();
        }

        public void OnHoverStart() =>
            (_activeState as IHoverStartListener)?.OnHoverStart();

        public void OnHoverEnd() =>
            (_activeState as IHoverEndListener)?.OnHoverEnd();
        
        public void Enter<TState>()
            where TState : class, ICraftingTableState
        {
            TState nextState = ChangeState<TState>();
            _activeState = nextState;
            (_activeState as IStartable)?.Start();
            EnterState?.Invoke(_activeState);
        }
        
        private TState ChangeState<TState>()
            where TState : class, ICraftingTableState
        {
            (_activeState as IExitable)?.Exit();
            ExitState?.Invoke(_activeState);
            TState nextState = _states[typeof(TState)] as TState;
            _activeState = nextState;
            return nextState;
        }
    }
}