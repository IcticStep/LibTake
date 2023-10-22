using System;
using Code.Runtime.Data;
using Code.Runtime.Infrastructure.Services.SaveLoad;

namespace Code.Runtime.Services.Player
{
    internal interface IPlayerInventoryService : IBookStorage, ISavedProgress
    {
    }
}