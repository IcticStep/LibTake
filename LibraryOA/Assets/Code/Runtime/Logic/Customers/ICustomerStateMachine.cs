using System;
using Code.Runtime.Logic.Customers.CustomersStates.Api;

namespace Code.Runtime.Logic.Customers
{
    public interface ICustomerStateMachine
    {
        IProgress Progress { get; }
        string ActiveStateName { get; }
        Type ActiveStateType { get; }

        void Enter<TState>()
            where TState : class, ICustomerState;

        void Enter<TState, TPayload>(TPayload payload)
            where TState : class, IPayloadedCustomerState<TPayload>;
    }
}