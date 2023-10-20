using Code.Runtime.Data;

namespace Code.Runtime.Logic.Interactions
{
    internal interface IBookStorageHolder
    {
        IBookStorage BookStorage { get; }
    }
}