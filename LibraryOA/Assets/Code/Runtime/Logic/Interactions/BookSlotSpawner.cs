using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    [RequireComponent(typeof(UniqueId))]
    public sealed class BookSlotSpawner : MonoBehaviour
    {
        [SerializeField] private StaticBook _initialBook;
        private string _bookSlotId;
        private IGameFactory _gameFactory;
        private Transform _transform;

        [Inject]
        private void Construct(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        private void Awake()
        {
            _transform = transform;
            _bookSlotId = GetComponent<UniqueId>().Id;
        }

        private void Start() =>
            // ReSharper disable once Unity.NoNullPropagation
            _gameFactory.CreateBookSlot(_bookSlotId, _transform.position, _transform, _initialBook?.Id);
    }
}