using System;
using Code.Runtime.Logic.Customers;
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

        /// <summary>
        /// Creates maximum amount in pull and set in spawn position in deactivated state. 
        /// </summary>
        void CreateCustomers();

        /// <summary>
        /// Returns active customers if any c
        /// Throws <see cref="InvalidOperationException"/> if trying to get more than <see cref="CustomersPoolingService.ActiveLimit"/> customers.
        /// </summary>
        /// <param name="posiotion"></param>
        CustomerStateMachine GetCustomer(Vector3 position);

        void ReturnCustomer(CustomerStateMachine customer);
    }
}