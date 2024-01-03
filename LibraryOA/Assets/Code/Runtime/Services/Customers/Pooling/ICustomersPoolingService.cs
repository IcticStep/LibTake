using System;
using Code.Runtime.Logic.Customers;
using Code.Runtime.Logic.Customers.CustomersStates.Api;
using UnityEngine;

namespace Code.Runtime.Services.Customers.Pooling
{
    internal interface ICustomersPoolingService
    {
        int ActiveHardLimit { get; }

        /// <summary>
        /// In range [0, <see cref="ActiveHardLimit"/>>].
        /// </summary>
        int ActiveLimit { get; }

        int ActiveCustomers { get; }
        int DeactivatedCustomers { get; }
        bool CanActivateMore();

        /// <summary>
        /// Creates maximum amount in pull and set in spawn position in deactivated state. 
        /// </summary>
        void CreateCustomers();

        /// <summary>
        /// Returns active customers if any c
        /// Throws <see cref="InvalidOperationException"/> if trying to get more than <see cref="CustomersPoolingService.ActiveLimit"/> customers.
        /// </summary>
        /// <param name="posiotion"></param>
        ICustomerStateMachine GetCustomer(Vector3 position);

        public void ReturnCustomer(CustomerStateMachine customer);
        void CleanUp();
    }
}