using System;
using Code.Runtime.Infrastructure.Services.PlayerProvider;
using Code.Runtime.Player;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactions
{
    internal sealed class BookTableView : MonoBehaviour
    {
        [SerializeField] private BookTable _bookTable;
        private IPlayerProviderService _playerProviderService;
        private InteractablesScanner _interactablesScanner;

        [Inject]
        private void Construct(IPlayerProviderService playerProviderService) =>
            _playerProviderService = playerProviderService;

        private void Start()
        {
            if(_playerProviderService.Player is null) 
                return;
            
            _interactablesScanner = _playerProviderService.Player.GetComponent<InteractablesScanner>();
            _interactablesScanner.FocusedInteractable += OnInteractableFocused;
            _interactablesScanner.UnfocusedInteractable += OnInteractableUnfocused;
        }

        private void OnDestroy()
        {
            if(_playerProviderService.Player is null) 
                return;

            _interactablesScanner.FocusedInteractable -= OnInteractableFocused;
            _interactablesScanner.UnfocusedInteractable -= OnInteractableUnfocused;
        }

        private void OnInteractableFocused(Interactable interactable)
        {
            if(interactable != _bookTable)
                return;
            Debug.Log("Table focused.");
        }

        private void OnInteractableUnfocused(Interactable interactable)
        {
            if(interactable != _bookTable)
                return;
            Debug.Log("Table unfocused.");
        }
    }
}