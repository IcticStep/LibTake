using System;
using Code.Runtime.Infrastructure.GameStates.Api;

namespace Code.Runtime.Infrastructure.GameStates.Factories
{
    public interface IGameStateFactory
    {
        IExitableState Create(Type type);

        T GetState<T>()
            where T : class, IExitableState;
    }
}