using System;
using System.Collections.Generic;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Customers.CustomersStates;
using Code.Runtime.Logic.Interactions;
using Code.Runtime.Services.BooksReceiving;
using Code.Runtime.Services.Customers.Queue;
using Code.Runtime.Services.Player;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.Services.Player.Lives;
using UnityEngine;
using UnityEngine.Serialization;
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
        [FormerlySerializedAs("_bookStorageHolder")]
        [SerializeField]
        private BookStorage _bookStorage;

        private Dictionary<Type, ICustomerState> _states;
        private ICustomerState _activeState;
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
            _states = new Dictionary<Type, ICustomerState>
            {
                [typeof(QueueMemberState)] = new QueueMemberState(this, _queueMember, _customersQueueService, _customerNavigator),
                [typeof(BookReceivingState)] = new BookReceivingState(this, _customersQueueService, _booksReceivingService, _bookReceiver,
                    _progress, _staticDataService, _collider, _playerInventoryService, _playerLivesService),
                [typeof(GoAwayState)] = new GoAwayState(this, _staticDataService, _customerNavigator),
                [typeof(DeactivatedState)] = new DeactivatedState(_queueMember, _bookStorage, _bookReceiver),
            };

        public void Enter<TState>()
            where TState : ICustomerState
        {
            _activeState?.Exit();
            ICustomerState nextState = _states[typeof(TState)];
            _activeState = nextState;
            _activeState.Start();
            
            if(_activeState is DeactivatedState)
                DeactivateStateEntered?.Invoke(this);
        }
    }
}