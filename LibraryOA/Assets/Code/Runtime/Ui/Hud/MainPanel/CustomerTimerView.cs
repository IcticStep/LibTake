using System.Collections;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Customers;
using Code.Runtime.Logic.Customers.CustomersStates;
using Code.Runtime.Services.Customers.Queue;
using Code.Runtime.Services.Customers.Registry;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Ui.Hud.MainPanel
{
    internal sealed class CustomerTimerView : MonoBehaviour
    {
        [SerializeField]
        private ProgressBar _progressBar;
        [SerializeField]
        private float _updateInterval = 0.1f;

        private ICustomersQueueService _customersQueueService;
        private ICustomersRegistryService _customersRegistryService;
        private WaitForSeconds _waitCached;

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
            StartCoroutine(UpdateViewCoroutine());

        private IEnumerator UpdateViewCoroutine()
        {
            while(true)
            {
                yield return _waitCached;
                UpdateProgressBar();
            }
        }

        private void UpdateProgressBar()
        {
            if(!_customersQueueService.Any)
            {
                _progressBar.SetProgress(0, 1);
                return;
            }

            ICustomerStateMachine firstCustomer = GetFirstCustomer();
            if(firstCustomer.ActiveStateType != typeof(BookReceivingState))
            {
                _progressBar.SetProgress(0, 1);
                return;
            }

            IProgress progress = firstCustomer.Progress;
            _progressBar.SetProgress(progress.Value, progress.MaxValue);
        }

        private ICustomerStateMachine GetFirstCustomer()
        {
            QueueMember firstCustomerMember = _customersQueueService.Peek();
            return _customersRegistryService.GetCustomerByQueueMember(firstCustomerMember);
        }
    }
}