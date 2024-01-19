using Code.Runtime.Infrastructure.Services.Camera;
using Code.Runtime.Services.Player.Provider;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Triggers
{
    internal sealed class CameraTargetSwitchTrigger : MonoBehaviour
    {
        [SerializeField]
        private Trigger _trigger;
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private float _animationDuration;
        
        private IPlayerProviderService _playerProviderService;
        private ICameraProvider _cameraProvider;

        [Inject]
        private void Construct(IPlayerProviderService playerProviderService, ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
            _playerProviderService = playerProviderService;
        }
        
        private void Awake()
        {
            _trigger.Entered += FollowTarget;
            _trigger.Exited += FollowPlayer;
        }

        private void OnDestroy()
        {
            _trigger.Entered -= FollowTarget;
            _trigger.Exited -= FollowPlayer;
        }

        private void FollowTarget() =>
            _cameraProvider
                .CameraFollow
                .MoveToNewTarget(_target, _animationDuration);

        private void FollowPlayer() =>
            _cameraProvider
                .CameraFollow
                .MoveToNewTarget(_playerProviderService.Transform, _animationDuration);
    }
}