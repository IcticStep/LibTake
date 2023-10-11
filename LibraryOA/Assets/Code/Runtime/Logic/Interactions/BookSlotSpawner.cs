using Code.Runtime.Infrastructure.Services.Factories;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    [RequireComponent(typeof(UniqueId))]
    public sealed class BookSlotSpawner : MonoBehaviour
    {
        private string _id;
        private IGameFactory _gameFactory;

        [Inject]
        private void Construct(IGameFactory gameFactory) =>
            _gameFactory = gameFactory;

        private void Awake() =>
            _id = GetComponent<UniqueId>().Id;

        private void Start()
        {
            Interactable interactable = _gameFactory
                .CreateBookSlot(transform.position)
                .GetComponentInChildren<Interactable>();

            ((IUniqueIdInitializer)interactable).Id = _id;
        }
    }
}