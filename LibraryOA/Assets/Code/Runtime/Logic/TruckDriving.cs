using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.StaticData.Level.MarkersStaticData;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Runtime.Logic
{
    [SelectionBase]
    public sealed class TruckDriving : MonoBehaviour
    {
        private IStaticDataService _staticDataService;
        private float _drivingSeconds;
        private Tween _driveToLibraryTween;
        private Tween _driveAwayLibraryTween;
        private TruckWayStaticData _way;

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _drivingSeconds = _staticDataService.Interactables.Truck.DrivingSeconds;

            _way = _staticDataService.CurrentLevelData.TruckWay;

            InitTweens();
        }

        public UniTask DriveToLibrary()
        {
            if(_driveToLibraryTween.IsPlaying())
                return UniTask.WaitWhile(_driveToLibraryTween.IsPlaying);

            _driveAwayLibraryTween.Pause();
            
            _driveToLibraryTween.Restart();
            return _driveToLibraryTween.AwaitForComplete();
        }
        
        public UniTask DriveAwayLibrary()
        {
            if(_driveAwayLibraryTween.IsPlaying())
                return UniTask.WaitWhile(_driveAwayLibraryTween.IsPlaying);
            
            _driveToLibraryTween.Pause();
            
            _driveAwayLibraryTween.Restart();
            return _driveAwayLibraryTween.AwaitForComplete();
        }

        private void InitTweens()
        {
            _driveToLibraryTween = gameObject.transform
                .DOMove(_way.LibraryPoint.Position, _drivingSeconds)
                .SetEase(Ease.OutCirc)
                .SetAutoKill(false)
                .Pause();

            _driveAwayLibraryTween = gameObject.transform
                .DOMove(_way.HiddenPoint.Position, _drivingSeconds)
                .SetEase(Ease.InCirc)
                .SetAutoKill(false)
                .Pause();
        }
    }
}