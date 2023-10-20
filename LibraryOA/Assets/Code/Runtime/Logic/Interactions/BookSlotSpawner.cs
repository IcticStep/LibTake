using Code.Runtime.Infrastructure.Services.Factories;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    [RequireComponent(typeof(UniqueId))]
    public sealed class BookSlotSpawner : MonoBehaviour
    {
        [SerializeField] private string _initialBookId;
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
            _gameFactory.CreateBookSlot(_bookSlotId, _transform.position, _transform, _initialBookId);
    }
}