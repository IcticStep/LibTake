using System;
using Code.Runtime.Logic.Customers;
using Code.Runtime.Logic.Customers.CustomersStates;
using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Code.Runtime.Ui.Behaviours;
using UnityEngine;

namespace Code.Runtime.Ui
{
    internal sealed class CustomerCoin : MonoBehaviour
    {
        [SerializeField]
        private SmoothFader _smoothFader;
        [SerializeField]
        private MoverY _moverY;
        [SerializeField]
        private CustomerStateMachine _customer;

        private void Awake() =>
            _customer.StateEntered += OnStateChanged;

        private void Start() =>
            _smoothFader.FadeImmediately();

        private void OnDestroy() =>
            _customer.StateEntered -= OnStateChanged;

        private void OnStateChanged(CustomerStateMachine customer, IExitableCustomerState state)
        {
            if(state is RewardState)
                ShowCoin();
        }

        private void ShowCoin()
        {
            _smoothFader.UnFadeImmediately();
            //_smoothFader.Fade();
            _moverY.Move();
        }
    }
}