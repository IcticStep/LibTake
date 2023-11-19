using System;
using System.Collections.Generic;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Infrastructure.States.Exceptions;
using JetBrains.Annotations;
using Zenject;

namespace Code.Runtime.Infrastructure.States
{
    [UsedImplicitly]
    internal sealed class GameStateFactory : IFactory<Type, IExitableState>, IGameStateFactory
    {
        private readonly Dictionary<Type, Func<IExitableState>> _statesResolvers;
        
        public GameStateFactory(DiContainer container)
        {
            _statesResolvers = new Dictionary<Type, Func<IExitableState>>
            {
                [typeof(BootstrapState)] = container.Resolve<BootstrapState>,
                [typeof(WarmupState)] = container.Resolve<WarmupState>,
                [typeof(LoadProgressState)] = container.Resolve<LoadProgressState>,
                [typeof(LoadLevelState)] = container.Resolve<LoadLevelState>,
                [typeof(GameLoopState)] = container.Resolve<GameLoopState>,
            };
        }

        public IExitableState Create(Type type)
        {
            if (!_statesResolvers.TryGetValue(type, out Func<IExitableState> resolver))
                throw new InvalidGameStateRequestException(type.Name);

            return resolver();
        }

        public T GetState<T>()
            where T : class, IExitableState =>
            Create(typeof(T)) as T;
    }
}