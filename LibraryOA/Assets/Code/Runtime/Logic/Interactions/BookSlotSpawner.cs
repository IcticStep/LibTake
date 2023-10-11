using Code.Runtime.Infrastructure.Services.Factories;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    [RequireComponent(typeof(UniqueId))]
    public sealed class BookSlotSpawner : MonoBehaviour
    {
        [SerializeField] private bool _hasBook = true;
        private string _id;
        private IGameFactory _gameFactory;
        private Transform _transform;

        [Inject]
        private void Construct(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        private void Awake()
        {
            _transform = transform;
            _id = GetComponent<UniqueId>().Id;
        }

        private void Start() =>
            _gameFactory.CreateBookSlot(_id, _hasBook, _transform.position, _transform);
    }
}