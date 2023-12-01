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
    internal sealed class TruckDriving : MonoBehaviour
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

            string currentLevel = SceneManager.GetActiveScene().name;
            _way = _staticDataService.ForLevel(currentLevel).TruckWay;

            InitTweens();
        }

        public UniTask DriveToLibrary()
        {
            if(_driveToLibraryTween.IsPlaying())
                return UniTask.WaitWhile(_driveToLibraryTween.IsPlaying);

            _driveAwayLibraryTween.Pause();
            
            _driveToLibraryTween.Restart();
            return _driveToLibraryTween.ToUniTask();
        }
        
        public UniTask DriveAwayLibrary()
        {
            if(_driveAwayLibraryTween.IsPlaying())
                return UniTask.WaitWhile(_driveAwayLibraryTween.IsPlaying);
            
            _driveToLibraryTween.Pause();
            
            _driveAwayLibraryTween.Restart();
            return _driveAwayLibraryTween.ToUniTask();
        }

        private void InitTweens()
        {
            _driveToLibraryTween = gameObject.transform
                .DOMove(_way.LibraryPoint.Position, _drivingSeconds)
                .SetEase(Ease.OutCirc)
                .SetAutoKill(false)
                .Pause();

            _driveAwayLibraryTween = gameObject.transform
                .DOMove(_way.LibraryPoint.Position, _drivingSeconds)
                .SetEase(Ease.InCirc)
                .SetAutoKill(false)
                .Pause();
        }
    }
}