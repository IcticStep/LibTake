using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    [RequireComponent(typeof(UniqueId))]
    public sealed class BookSlotMarker : MonoBehaviour
    {
        [SerializeField] private StaticBook _initialBook;
        public string InitialBookId => _initialBook.Id;
    }
}