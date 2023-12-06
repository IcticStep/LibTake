using System;
using System.Collections.Generic;
using Code.Runtime.Infrastructure.States.Api;
using JetBrains.Annotations;
using IState = Code.Runtime.Infrastructure.States.Api.IState;

namespace Code.Runtime.Infrastructure.States
{
    [UsedImplicitly]
    internal sealed class GameStateMachine
    {
        private readonly IGameStateFactory _gameStateFactory;
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public Type ActiveStateType => _activeState.GetType();

        public GameStateMachine(IGameStateFactory gameStateFactory)
        {
            _gameStateFactory = gameStateFactory;
        }

        public void EnterState<TState>()
            where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Start();
        }
        
        public void EnterState<TState, TPayload>(TPayload payload)
            where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Start(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _gameStateFactory.GetState<TState>();
    }
}