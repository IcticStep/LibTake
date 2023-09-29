using System;
using Code.Runtime.Infrastructure.DiInstallers;

namespace Code.Runtime.Infrastructure.States.Exceptions
{
    internal sealed class InvalidGameStateRequestException : Exception
    {
        public InvalidGameStateRequestException(string nameOfState) 
            : base($"Invalid game state requested. State {nameOfState} is unknown. Check registration in" +
                   $"{nameof(BootstrapInstaller)} and {nameof(GameStateFactory)}.")
        {
        }
    }
}