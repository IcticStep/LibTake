using System;
using System.Collections;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Customers;
using Code.Runtime.Logic.Customers.CustomersStates;
using Code.Runtime.Services.Customers.Queue;
using Code.Runtime.Services.Customers.Registry;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.HudComponents.MainPanel
{
    internal sealed class CustomersTimerSource : MonoBehaviour
    {
        [SerializeField]
        private float _updateInterval = 0.1f;

        private ICustomersQueueService _customersQueueService;
        private ICustomersRegistryService _customersRegistryService;
        private WaitForSeconds _waitCached;

        public event Action<float> Updated;
        
        [Inject]
        private void Construct(ICustomersQueueService customersQueueService,
            ICustomersRegistryService customersRegistryService)
        {
            _customersRegistryService = customersRegistryService;
            _customersQueueService = customersQueueService;
        }
        
        private void Awake() =>
            _waitCached = new WaitForSeconds(_updateInterval);
        
        private void Start() =>
            StartCoroutine(UpdateSourceCoroutine());
        
        private IEnumerator UpdateSourceCoroutine()
        {
            while(true)
            {
                yield return _waitCached;
                UpdateTimer();
            }
        }

        private void UpdateTimer()
        {
            if(!_customersQueueService.Any)
            {
                Updated?.Invoke(0);
                return;
            }

            ICustomerStateMachine firstCustomer = GetFirstCustomer();
            if(firstCustomer.ActiveStateType != typeof(BookReceivingState))
            {
                Updated?.Invoke(0);
                return;
            }

            IProgress progress = firstCustomer.Progress;
            Updated?.Invoke(progress.Value);
        }

        private ICustomerStateMachine GetFirstCustomer()
        {
            QueueMember firstCustomerMember = _customersQueueService.Peek();
            return _customersRegistryService.GetCustomerByQueueMember(firstCustomerMember);
        }
    }
}