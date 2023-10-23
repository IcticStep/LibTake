using System;
using Code.Runtime.Data;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Logic.Interactions.Data;

namespace Code.Runtime.Services.Player
{
    internal interface IPlayerInventoryService : IBookStorage, ISavedProgress
    {
    }
}