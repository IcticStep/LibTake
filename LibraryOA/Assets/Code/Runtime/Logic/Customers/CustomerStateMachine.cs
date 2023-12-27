using System;
using System.Collections.Generic;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Customers.CustomersStates;
using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Code.Runtime.Logic.Interactions;
using Code.Runtime.Services.BooksReceiving;
using Code.Runtime.Services.Customers.Queue;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.Services.Player.Lives;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Customers
{
    [SelectionBase]
    public sealed class CustomerStateMachine : MonoBehaviour
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

        public Progress Progress => _progress;

        public string ActiveStateName => _activeState == null ? "none" : _activeState.ToString();
        public Type ActiveStateType => _activeState.GetType();
        public event Action<CustomerStateMachine> DeactivateStateEntered;

        [Inject]
        private void Construct(ICustomersQueueService customersQueueService, IStaticDataService staticDataService, IBooksReceivingService booksReceivingService,
            IPlayerInventoryService playerInventoryService, IPlayerLivesService playerLivesService)
        {
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
                [typeof(BookReceivingState)] = new BookReceivingState(this, _customersQueueService, _booksReceivingService, _bookReceiver,
                    _progress, _staticDataService, _collider),
                [typeof(GoAwayState)] = new GoAwayState(this, _staticDataService, _customerNavigator),
                [typeof(RewardState)] = new RewardState(this, _bookReceiver, _progress, _playerLivesService, _playerInventoryService, _staticDataService),
                [typeof(DeactivatedState)] = new DeactivatedState(_queueMember, _bookStorage, _bookReceiver),
            };

        public void Enter<TState>()
            where TState : class, ICustomerState
        {
            TState nextState = ChangeState<TState>();
            nextState.Start();

            SignalChanged();
        }
        
        public void Enter<TState, TPayload>(TPayload payload)
            where TState : class, IPayloadedCustomerState<TPayload>
        {
            TState nextState = ChangeState<TState>();
            nextState.Start(payload);

            SignalChanged();
        }

        private TState ChangeState<TState>()
            where TState : class, IExitableCustomerState
        {
            _activeState?.Exit();
            TState nextState = _states[typeof(TState)] as TState;
            _activeState = nextState;
            return nextState;
        }

        private void SignalChanged()
        {
            if(_activeState is DeactivatedState)
                DeactivateStateEntered?.Invoke(this);
        }
    }
}