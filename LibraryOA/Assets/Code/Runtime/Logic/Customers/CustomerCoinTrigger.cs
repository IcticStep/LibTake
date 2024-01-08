using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Code.Runtime.Ui.FlyingResources;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic.Customers
{
    public class CustomerCoinTrigger : MonoBehaviour
    {
        [SerializeField]
        private CustomerStateMachine _customer;
        [SerializeField]
        private FlyingResource _flyingResource;
        
        private void Awake()
        {
            _customer.StateEntered += OnStateChanged;
            _customer.StateExited += OnStateExited;
        }

        private void OnDestroy()
        {
            _customer.StateEntered -= OnStateChanged;
            _customer.StateExited -= OnStateExited;
        }

        private void OnStateChanged(CustomerStateMachine customer, IExitableCustomerState state)
        {
            if(state is IRewardSource coinRewardSource)
                coinRewardSource.Rewarded += ShowCoin;
        }

        private void OnStateExited(CustomerStateMachine customer, IExitableCustomerState state)
        {
            if(state is IRewardSource coinRewardSource)
                coinRewardSource.Rewarded -= ShowCoin;
        }

        private void ShowCoin(int coinsAmount) =>
            _flyingResource
                .FlyResource(coinsAmount)
                .Forget();
    }
}