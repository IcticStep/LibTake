using System;
using System.Collections.Generic;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Logic.Interactables.Api;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.Services.Player.Provider;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Crafting
{
    public sealed class CraftingTableStateMachine : Interactable, IHoverStartListener, IHoverEndListener, ISavedProgress
    {
        [SerializeField]
        private Progress _progress;

        private ICraftingTableState _activeState;
        private Dictionary<Type, ICraftingTableState> _states;
        private ICraftingService _craftingService;
        private IPlayerProviderService _playerProviderService;

        public string ActiveStateName => _activeState is null ? "none" : _activeState.ToString();

        public event Action<ICraftingTableState> EnterState;
        public event Action<ICraftingTableState> ExitState;

        [Inject]
        private void Construct(ICraftingService craftingService, IPlayerProviderService playerProviderService)
        {
            _playerProviderService = playerProviderService;
            _craftingService = craftingService;
        }

        private void Awake() =>
            _states = new Dictionary<Type, ICraftingTableState>()
            {
                [typeof(PayState)] = new PayState(this, _craftingService),
                [typeof(SkillCheckState)] = new SkillCheckState(this, _craftingService),
                [typeof(CraftingState)] = new CraftingState(this, _craftingService, _progress, _playerProviderService),
                [typeof(FinishCraftState)] = new FinishCraftState(this, _craftingService),
            };

        private void Start()
        {
            if(_activeState is null)
                Enter<PayState>();
        }

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
            ExitCurrentState();
            TState nextState = GetState<TState>();
            StartState(nextState);
        }

        private void Enter(Type stateType)
        {
            ExitCurrentState();
            ICraftingTableState nextState = GetState(stateType);
            StartState(nextState);
        }

        private void ExitCurrentState()
        {
            (_activeState as IExitable)?.Exit();
            ExitState?.Invoke(_activeState);
        }

        private TState GetState<TState>()
            where TState : class, ICraftingTableState =>
            _states[typeof(TState)] as TState;

        private ICraftingTableState GetState(Type stateType) =>
            _states[stateType];

        private void StartState(ICraftingTableState nextState)
        {
            _activeState = nextState;
            (_activeState as IStartable)?.Start();
            EnterState?.Invoke(_activeState);
        }

        public void LoadProgress(GameProgress progress)
        {
            Type savedStateType = progress.WorldData.CraftingTableStates.GetDataForId(Id);
            if(savedStateType is null)
                return;
            
            Enter(savedStateType);
        }

        public void UpdateProgress(GameProgress progress) =>
            progress.WorldData.CraftingTableStates.SetDataForId(Id, _activeState.GetType());
    }
}