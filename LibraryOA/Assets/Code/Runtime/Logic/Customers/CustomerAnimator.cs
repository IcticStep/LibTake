using Code.Runtime.Logic.Interactions;
using UnityEngine;

namespace Code.Runtime.Logic.Customers
{
    internal sealed class CustomerAnimator : MonoBehaviour
    {
        private static readonly int _speedParameter = Animator.StringToHash("SpeedPercents");

        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private BookStorageHolder _bookStorageHolder;
        [SerializeField]
        private CustomerNavigator _customerNavigator;
        
        private int _handleItemLayer;

        private void OnValidate() =>
            _animator ??= GetComponentInChildren<Animator>();

        private void Awake() =>
            _handleItemLayer = _animator.GetLayerIndex("HandleItem");

        private void Start()
        {
            _bookStorageHolder.Updated += UpdateAnimatorBook;
            UpdateAnimatorSpeed();
            UpdateAnimatorBook();
        }

        private void Update() =>
            UpdateAnimatorSpeed();

        private void OnDestroy() =>
            _bookStorageHolder.Updated -= UpdateAnimatorBook;

        private void UpdateAnimatorSpeed() =>
            _animator.SetFloat(_speedParameter, _customerNavigator.SpeedPercents);

        private void UpdateAnimatorBook() =>
            _animator.SetLayerWeight(_handleItemLayer,
                _bookStorageHolder.HasBook 
                    ? 1
                    : 0);
    }
}