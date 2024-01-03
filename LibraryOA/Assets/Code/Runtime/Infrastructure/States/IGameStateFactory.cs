using System;
using Code.Runtime.Infrastructure.States.Api;

namespace Code.Runtime.Infrastructure.States
{
    public interface IGameStateFactory
    {
        IExitableState Create(Type type);

        T GetState<T>()
            where T : class, IExitableState;
    }
}