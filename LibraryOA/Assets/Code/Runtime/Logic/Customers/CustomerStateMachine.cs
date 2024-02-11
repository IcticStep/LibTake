using System;
using System.Collections.Generic;
using Code.Runtime.Infrastructure.GameStates;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Customers.CustomersStates;
using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Code.Runtime.Logic.Interactables;
using Code.Runtime.Services.Books.Receiving;
using Code.Runtime.Services.Books.Reward;
using Code.Runtime.Services.Customers.Queue;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.Services.Player.Lives;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Customers
{
    [SelectionBase]
    public sealed class CustomerStateMachine : MonoBehaviour, ICustomerStateMachine
    {
        [SerializeField]
        private QueueMember _queueMember;
        [SerializeField]
        private CustomerNavigator _customerNavigator;
        [SerializeField]
        private BookReceiver _bookReceiver;
        [SerializeField]
        private Progress _progress;
        [SerializeField]
        private Collider _collider;
        [SerializeField]
        private BookStorage _bookStorage;

        private Dictionary<Type, IExitableCustomerState> _states;
        private IExitableCustomerState _activeState;
        private ICustomersQueueService _customersQueueService;
        private IStaticDataService _staticDataService;
        private IBooksReceivingService _booksReceivingService;
        private IPlayerInventoryService _playerInventoryService;
        private IPlayerLivesService _playerLivesService;
        private IBookRewardService _bookRewardService;
        private GameStateMachine _gameStateMachine;

        public IProgress Progress => _progress;

        public string ActiveStateName => _activeState == null ? "none" : _activeState.ToString();
        public Type ActiveStateType => _activeState.GetType();

        public event Action<CustomerStateMachine, IExitableCustomerState> StateEntered;
        public event Action<CustomerStateMachine, IExitableCustomerState> StateExited;

        [Inject]
        private void Construct(ICustomersQueueService customersQueueService, IStaticDataService staticDataService, IBooksReceivingService booksReceivingService,
            IPlayerInventoryService playerInventoryService, IPlayerLivesService playerLivesService, IBookRewardService bookRewardService,
            GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _bookRewardService = bookRewardService;
            _playerLivesService = playerLivesService;
            _playerInventoryService = playerInventoryService;
            _booksReceivingService = booksReceivingService;
            _staticDataService = staticDataService;
            _customersQueueService = customersQueueService;
        }

        private void Awake() =>
            _states = new Dictionary<Type, IExitableCustomerState>
            {
                [typeof(QueueMemberState)] = new QueueMemberState(this, _queueMember, _customersQueueService, _customerNavigator),
                [typeof(BookReceivingState)] = new BookReceivingState(this, _booksReceivingService, _bookReceiver,
                    _progress, _staticDataService, _collider),
                [typeof(RewardState)] = new RewardState(this, _progress, _playerInventoryService, _bookRewardService, _staticDataService),
                [typeof(PunishState)] = new PunishState(this, _bookReceiver, _progress, _playerLivesService),
                [typeof(GoAwayState)] = new GoAwayState(this, _staticDataService, _customerNavigator, _customersQueueService, _gameStateMachine),
                [typeof(DeactivatedState)] = new DeactivatedState(_queueMember, _bookStorage, _bookReceiver),
            };

        public void Enter<TState>()
            where TState : class, ICustomerState
        {
            TState nextState = ChangeState<TState>();
            SignalChanged();
            nextState.Start();
        }
        
        public void Enter<TState, TPayload>(TPayload payload)
            where TState : class, IPayloadedCustomerState<TPayload>
        {
            TState nextState = ChangeState<TState>();
            SignalChanged();
            nextState.Start(payload);
        }

        public void ForceStop()
        {
            if(_activeState is IForceStoppable forceStoppableState)
                forceStoppableState.ForceStop();
        }

        private TState ChangeState<TState>()
            where TState : class, IExitableCustomerState
        {
            _activeState?.Exit();
            StateExited?.Invoke(this, _activeState);
            TState nextState = _states[typeof(TState)] as TState;
            _activeState = nextState;
            return nextState;
        }

        private void SignalChanged() =>
            StateEntered?.Invoke(this, _activeState);
    }
}