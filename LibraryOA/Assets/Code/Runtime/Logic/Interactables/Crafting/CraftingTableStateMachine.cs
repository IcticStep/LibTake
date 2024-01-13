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
    internal sealed class CraftingTableStateMachine : Interactable, IHoverStartListener, IHoverEndListener
    {
        [SerializeField]
        private Progress _progress;

        private ICraftingTableState _activeState;
        private Dictionary<Type, ICraftingTableState> _states;
        private ICraftingService _craftingService;

        public bool InProgress => _progress.Running;

        [Inject]
        public void Construct(ICraftingService craftingService) =>
            _craftingService = craftingService;

        private void Awake() =>
            _states = new Dictionary<Type, ICraftingTableState>()
            {
                [typeof(PayState)] = new PayState(this, _craftingService),
                [typeof(SkillCheckState)] = new SkillCheckState(this, _craftingService),
                [typeof(CraftingState)] = new CraftingState(this, _craftingService),
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
        }
        
        private TState ChangeState<TState>()
            where TState : class, ICraftingTableState
        {
            (_activeState as IExitable)?.Exit();
            TState nextState = _states[typeof(TState)] as TState;
            _activeState = nextState;
            return nextState;
        }
    }
}