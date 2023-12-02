using System;
using System.Collections.Generic;
using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Customers;
using Code.Runtime.Logic.Customers.CustomersStates;
using Code.Runtime.StaticData.Level;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Runtime.Services.Customers.Pooling
{
    [UsedImplicitly]
    internal sealed class CustomersPoolingService : ICustomersPoolingService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly InteractablesFactory _interactablesFactory;
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

        public CustomersPoolingService(IStaticDataService staticDataService, InteractablesFactory interactablesFactory)
        {
            _staticDataService = staticDataService;
            _interactablesFactory = interactablesFactory;
        }

        /// <summary>
        /// Creates maximum amount in pull and set in spawn position in deactivated state. 
        /// </summary>
        public void CreateCustomers()
        {
            LevelStaticData levelData = GetLevelData();
            SetSpawnLimits(levelData);

            for(int i = 0; i < ActiveLimit; i++)
            {
                CustomerStateMachine customer = _interactablesFactory.CreateCustomer(Spawn);
                _deactivatedCustomers.Push(customer);
            }
        }

        /// <summary>
        /// Returns active customers if any c
        /// Throws <see cref="InvalidOperationException"/> if trying to get more than <see cref="ActiveLimit"/> customers.
        /// </summary>
        public CustomerStateMachine GetCustomer()
        {
            if(ActiveCustomers >= ActiveLimit)
                throw new InvalidOperationException($"Can't activate more than {ActiveLimit} customers!");

            if(DeactivatedCustomers > 0)
            {
                CustomerStateMachine customer = _deactivatedCustomers.Pop();
                _activeCustomers.Add(customer);
                return customer;
            }

            CustomerStateMachine newCustomer = _interactablesFactory.CreateCustomer(Spawn);
            _activeCustomers.Add(newCustomer);
            return newCustomer;
        }

        public void ReturnCustomer(CustomerStateMachine customer)
        {
            if(!_activeCustomers.Contains(customer))
                Debug.LogWarning("Untracked customer by pool returned to pool", customer.gameObject);
            else
                _activeCustomers.Remove(customer);
            
            customer.Enter<DeactivatedState>();
            _deactivatedCustomers.Push(customer);
        }

        private void SetSpawnLimits(LevelStaticData levelData)
        {
            ActiveHardLimit = levelData.Customers.QueuePoints.Count;
            ActiveLimit = ActiveLimit == 0 ? ActiveHardLimit : ActiveLimit;
        }

        private Vector3 GetSpawnPoint() =>
            GetLevelData()
                .Customers
                .SpawnPoint;

        private LevelStaticData GetLevelData()
        {
            string level = SceneManager.GetActiveScene().name;
            return _staticDataService.ForLevel(level);
        }
    }
}