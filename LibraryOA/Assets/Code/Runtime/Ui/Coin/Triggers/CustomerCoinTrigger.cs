using Code.Runtime.Logic.Customers;
using Code.Runtime.Logic.Customers.CustomersStates;
using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Ui.Coin.Triggers
{
    public class CustomerCoinTrigger : MonoBehaviour
    {
        [SerializeField]
        private CustomerStateMachine _customer;
        [SerializeField]
        private FlyingCoin _flyingCoin;
        
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
            if(state is ICoinRewardSource coinRewardSource)
                coinRewardSource.Rewarded += ShowCoin;
        }

        private void OnStateExited(CustomerStateMachine customer, IExitableCustomerState state)
        {
            if(state is ICoinRewardSource coinRewardSource)
                coinRewardSource.Rewarded -= ShowCoin;
        }

        private void ShowCoin(int coinsAmount) =>
            _flyingCoin.ShowCoin(coinsAmount).Forget();
    }
}