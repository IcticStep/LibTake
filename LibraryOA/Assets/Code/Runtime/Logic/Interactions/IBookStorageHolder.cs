using Code.Runtime.Data;
using Code.Runtime.Logic.Interactions.Data;

namespace Code.Runtime.Logic.Interactions
{
    internal interface IBookStorageHolder
    {
        IBookStorage BookStorage { get; }
    }
}