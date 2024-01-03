using System;
using System.Collections.Generic;
using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Customers;
using Code.Runtime.Logic.Customers.CustomersStates;
using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Code.Runtime.StaticData.Level;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.Customers.Pooling
{
    [UsedImplicitly]
    internal sealed class CustomersPoolingService : ICustomersPoolingService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IInteractablesFactory _interactablesFactory;
        private readonly Stack<CustomerStateMachine> _deactivatedCustomers = new();
        private readonly HashSet<CustomerStateMachine> _activeCustomers = new();
        
        private int _activeLimit;
        private Vector3? _spawn;
        public int ActiveHardLimit { get; private set; }
        
        /// <summary>
        /// In range [0, <see cref="ActiveHardLimit"/>>].
        /// </summary>
        public int ActiveLimit
        {
            get => _activeLimit;
            private set => _activeLimit = Mathf.Clamp(value, 0, ActiveHardLimit);
        }

        public int ActiveCustomers => _activeCustomers.Count;
        public int DeactivatedCustomers => _deactivatedCustomers.Count;

        private Vector3 Spawn => _spawn ?? (_spawn = GetSpawnPoint()).Value;

        public CustomersPoolingService(IStaticDataService staticDataService, IInteractablesFactory interactablesFactory)
        {
            _staticDataService = staticDataService;
            _interactablesFactory = interactablesFactory;
        }

        /// <summary>
        /// Creates maximum amount in pull and set in spawn position in deactivated state. 
        /// </summary>
        public void CreateCustomers()
        {
            LevelStaticData levelData = _staticDataService.CurrentLevelData;
            SetSpawnLimits(levelData);

            for(int i = 0; i < ActiveLimit; i++)
            {
                CustomerStateMachine customer = _interactablesFactory.CreateCustomer(Spawn);
                customer.Enter<DeactivatedState>();
                customer.StateEntered += OnCustomerStateChanged;
                customer.gameObject.SetActive(false);
                _deactivatedCustomers.Push(customer);
            }
        }

        /// <summary>
        /// Returns active customers if any c
        /// Throws <see cref="InvalidOperationException"/> if trying to get more than <see cref="ActiveLimit"/> customers.
        /// </summary>
        public ICustomerStateMachine GetCustomer(Vector3 position)
        {
            if(ActiveCustomers >= ActiveLimit)
                throw new InvalidOperationException($"Can't activate more than {ActiveLimit} customers!");

            if(DeactivatedCustomers > 0)
            {
                CustomerStateMachine customer = _deactivatedCustomers.Pop();
                customer.gameObject.SetActive(true);
                _activeCustomers.Add(customer);
                customer.gameObject.transform.position = position;
                return customer;
            }

            CustomerStateMachine newCustomer = _interactablesFactory.CreateCustomer(position);
            _activeCustomers.Add(newCustomer);
            return newCustomer;
        }

        public void ReturnCustomer(CustomerStateMachine customer)
        {
            if(_deactivatedCustomers.Contains(customer))
            {
                Debug.LogWarning("Tried to return customer twice.", customer.gameObject);
                return;
            }
            
            if(!_activeCustomers.Contains(customer))
                Debug.LogWarning("Untracked customer by pool returned to pool.", customer.gameObject);
            else
                _activeCustomers.Remove(customer);
            
            if(customer.ActiveStateType != typeof(DeactivatedState))
                customer.Enter<DeactivatedState>();
            
            customer.gameObject.SetActive(false);
            _deactivatedCustomers.Push(customer);
        }

        public void CleanUp()
        {
            foreach(CustomerStateMachine customer in _activeCustomers)
                customer.StateEntered -= OnCustomerStateChanged;
            foreach(CustomerStateMachine customer in _deactivatedCustomers)
                customer.StateEntered -= OnCustomerStateChanged;
            
            _activeCustomers.Clear();
            _deactivatedCustomers.Clear();
            _activeLimit = 0;
            ActiveHardLimit = 0;
            _spawn = null;
        }

        public bool CanActivateMore()
            => ActiveCustomers < ActiveLimit;

        private void OnCustomerStateChanged(CustomerStateMachine customer, IExitableCustomerState state)
        {
            if(state is not DeactivatedState)
                return;
            
            ReturnCustomer(customer);
        }

        private void SetSpawnLimits(LevelStaticData levelData)
        {
            ActiveHardLimit = levelData.Customers.QueuePoints.Count;
            ActiveLimit = ActiveLimit == 0 ? ActiveHardLimit : ActiveLimit;
        }

        private Vector3 GetSpawnPoint() =>
            _staticDataService
                .CurrentLevelData
                .Customers
                .SpawnPoint;
    }
}