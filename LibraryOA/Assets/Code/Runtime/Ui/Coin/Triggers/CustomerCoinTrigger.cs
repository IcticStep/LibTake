using Code.Runtime.Logic.Customers;
using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.Ui.Coin.Triggers
{
    public class CustomerCoinTrigger : MonoBehaviour
    {
        [SerializeField]
        private CustomerStateMachine _customer;
        [FormerlySerializedAs("_flyingResoruce")]
        [FormerlySerializedAs("_flyingCoin")]
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