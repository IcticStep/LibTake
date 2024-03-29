using System;
using Code.Runtime.Logic.Player;
using Code.Runtime.Services.Player.Provider;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Interactables
{
    internal sealed class HoverView : MonoBehaviour
    {
        [SerializeField] private Interactable _interactable;
        [SerializeField] private Renderer[] _hoverMeshes;

        private IPlayerProviderService _playerProviderService;
        
        private InteractablesScanner InteractablesScanner => _playerProviderService.InteractablesScanner;

        public event Action Focused;
        public event Action Unfocused;

        [Inject]
        private void Construct(IPlayerProviderService playerProviderService) =>
            _playerProviderService = playerProviderService;

        private void Start()
        {
            InteractablesScanner.FocusedInteractable += OnInteractableFocused;
            InteractablesScanner.UnfocusedInteractable += OnInteractableUnfocused;
            
            HideHover();
        }
        
        private void OnInteractableFocused(Interactable interactable)
        {
            if(interactable.Id != _interactable.Id) 
                return;

            Focused?.Invoke();
            ShowHover();
        }

        private void OnInteractableUnfocused(Interactable interactable)
        {
            if(interactable.Id != _interactable.Id)
                return;
            
            Unfocused?.Invoke();
            HideHover();
        }

        private void ShowHover()
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for(int i = 0; i < _hoverMeshes.Length; i++)
                _hoverMeshes[i].enabled = true;
        }

        private void HideHover()
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for(int i = 0; i < _hoverMeshes.Length; i++)
                _hoverMeshes[i].enabled = false;
        }
    }
}