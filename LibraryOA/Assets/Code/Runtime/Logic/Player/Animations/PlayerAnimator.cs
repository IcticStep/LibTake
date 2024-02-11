using Code.Runtime.Logic.Interactables;
using Code.Runtime.Logic.Interactables.Api;
using Code.Runtime.Services.InputService;
using Code.Runtime.Services.Player.Inventory;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Player.Animations
{
    internal sealed class PlayerAnimator : MonoBehaviour
    {
        private const string HandleItemLayerName = "HandleItem";
        private const string DoLayerName = "Do";
        private static readonly int _speedParameter = Animator.StringToHash("SpeedPercents");

        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private InteractablesScanner _interactablesScanner;
        
        private IPlayerInventoryService _playerInventory;
        private IInputService _inputService;
        private int _handleItemLayer;
        private int _doLayerIndex;

        [Inject]
        private void Construct(IPlayerInventoryService playerInventory, IInputService inputService)
        {
            _playerInventory = playerInventory;
            _inputService = inputService;
        }

        private void OnValidate()
        {
            _animator ??= GetComponentInChildren<Animator>();
            _interactablesScanner ??= GetComponent<InteractablesScanner>();
        }

        private void Awake()
        {
            _handleItemLayer = _animator.GetLayerIndex(HandleItemLayerName);
            _doLayerIndex = _animator.GetLayerIndex(DoLayerName);
        }

        private void Start()
        {
            UpdateAnimatorSpeed();
            UpdateAnimatorBook();

            _playerInventory.BooksUpdated += UpdateAnimatorBook;
            _interactablesScanner.FocusedInteractable += UpdateDoAnimation;
        }

        private void Update()
        {
            UpdateAnimatorSpeed();
            UpdateDoAnimation(_interactablesScanner.CurrentFocusedInteractable);
        }

        private void OnDestroy() =>
            _playerInventory.BooksUpdated -= UpdateAnimatorBook;

        private void UpdateAnimatorSpeed() =>
            _animator.SetFloat(_speedParameter, _inputService.GetMovement().magnitude);

        private void UpdateAnimatorBook() =>
            _animator.SetLayerWeight(_handleItemLayer,
                _playerInventory.HasBook 
                    ? 1
                    : 0);

        private void UpdateDoAnimation(Interactable interactable)
        {
            // ReSharper disable once Unity.PerformanceCriticalCodeNullComparison
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