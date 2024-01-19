using System;
using System.Collections.Generic;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Interactables.Api;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.Services.Player.Provider;
using Code.Runtime.Ui.FlyingResources;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactables.Crafting
{
    public sealed class CraftingTableStateMachine : Interactable, IHoverStartListener, IHoverEndListener, ISavedProgress, IRewardSource 
    {
        [SerializeField]
        private Progress _progress;

        private ICraftingTableState _activeState;
        private Dictionary<Type, ICraftingTableState> _states;
        private ICraftingService _craftingService;
        private IPlayerProviderService _playerProviderService;
        private IStaticDataService _staticDataService;

        public string ActiveStateName => ActiveState is null ? "none" : ActiveState.ToString();
        public ICraftingTableState ActiveState => _activeState;

        public event Action<ICraftingTableState> EnterState;
        public event Action<ICraftingTableState> ExitState;
        public event Action Hovered;
        public event Action<int> Rewarded;

        [Inject]
        private void Construct(ICraftingService craftingService, IPlayerProviderService playerProviderService, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _playerProviderService = playerProviderService;
            _craftingService = craftingService;
        }

        private void Awake() =>
            _states = new Dictionary<Type, ICraftingTableState>()
            {
                [typeof(PayState)] = new PayState(this, _craftingService),
                [typeof(SkillCheckState)] = new SkillCheckState(this, _craftingService),
                [typeof(CraftingState)] = new CraftingState(this, _craftingService, _progress, _playerProviderService),
                [typeof(FinishCraftState)] = new FinishCraftState(this, _craftingService, _staticDataService),
            };

        private void Start()
        {
            if(ActiveState is null)
                Enter<PayState>();
        }

        public override bool CanInteract() =>
            ActiveState.CanInteract();

        public override void Interact()
        {
            if(!CanInteract())
                return;

            ActiveState.Interact();
        }

        public void OnHoverStart()
        {
            (ActiveState as IHoverStartListener)?.OnHoverStart();
            Hovered?.Invoke();
        }

        public void OnHoverEnd() =>
            (ActiveState as IHoverEndListener)?.OnHoverEnd();

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

        private TState GetState<TState>()
            where TState : class, ICraftingTableState =>
            _states[typeof(TState)] as TState;

        private ICraftingTableState GetState(Type stateType) =>
            _states[stateType];

        private void StartState(ICraftingTableState nextState)
        {
            _activeState = nextState;
            (ActiveState as IStartable)?.Start();
            
            if(ActiveState is IRewardSource rewardSource)
                rewardSource.Rewarded += NotifyRewarded;
            
            EnterState?.Invoke(ActiveState);
        }

        private void ExitCurrentState()
        {
            (ActiveState as IExitable)?.Exit();
            
            if(ActiveState is IRewardSource rewardSource)
                rewardSource.Rewarded -= NotifyRewarded;
            
            ExitState?.Invoke(ActiveState);
        }

        private void NotifyRewarded(int amount) =>
            Rewarded?.Invoke(amount);

        public void LoadProgress(GameProgress progress)
        {
            Type savedStateType = progress.WorldData.CraftingTableStates.GetDataForId(Id);
            if(savedStateType is null)
                return;
            
            Enter(savedStateType);
        }

        public void UpdateProgress(GameProgress progress) =>
            progress.WorldData.CraftingTableStates.SetDataForId(Id, ActiveState.GetType());
    }
}