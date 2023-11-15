using System;
using Code.Runtime.Logic.Interactions;
using Code.Runtime.Logic.Interactions.Api;
using Code.Runtime.Services.Player;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Player
{
    internal sealed class PlayerAnimator : MonoBehaviour
    {
        private static readonly int _speedParameter = Animator.StringToHash("SqrSpeed");

        [SerializeField]
        private PlayerMove _playerMove;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private InteractablesScanner _interactablesScanner;
        
        private IPlayerInventoryService _playerInventory;
        private int _handleItemLayer;
        private int _doLayerIndex;

        [Inject]
        private void Construct(IPlayerInventoryService playerInventory) =>
            _playerInventory = playerInventory;

        private void OnValidate()
        {
            _playerMove ??= GetComponent<PlayerMove>();
            _animator ??= GetComponentInChildren<Animator>();
            _interactablesScanner ??= GetComponent<InteractablesScanner>();
        }

        private void Awake()
        {
            _handleItemLayer = _animator.GetLayerIndex("HandleItem");
            _doLayerIndex = _animator.GetLayerIndex("Do");
        }

        private void Start()
        {
            UpdateAnimatorSpeed();
            UpdateAnimatorBook();

            _playerInventory.Updated += UpdateAnimatorBook;
            _interactablesScanner.FocusedInteractable += UpdateDoAnimation;
        }

        private void Update()
        {
            UpdateAnimatorSpeed();
            UpdateDoAnimation(_interactablesScanner.CurrentFocusedInteractable);
        }

        private void OnDestroy()
        {
            _playerInventory.Updated -= UpdateAnimatorBook;
        }

        private void UpdateAnimatorSpeed()
        {
            float sqrMagnitude = _playerMove.CurrentHorizontalVelocity.sqrMagnitude;
            _animator.SetFloat(_speedParameter, sqrMagnitude);
        }

        private void UpdateAnimatorBook() =>
            _animator.SetLayerWeight(_handleItemLayer,
                _playerInventory.HasBook 
                    ? 1
                    : 0);

        private void UpdateDoAnimation(Interactable interactable)
        {
            if(interactable == null)
            {
                StopDoAnimation();
                return;
            }

            if(interactable is not IProgressOwner progressOwner)
            {
                StopDoAnimation();
                return;
            }

            if(!progressOwner.InProgress)
            {
                StopDoAnimation();
                return;
            }
            
            StartDoAnimation();
        }

        private void StartDoAnimation() =>
            _animator.SetLayerWeight(_doLayerIndex, 1);

        private void StopDoAnimation() =>
            _animator.SetLayerWeight(_doLayerIndex, 0);
    }
}