using Code.Runtime.Logic.GlobalGoals.RocketStart;
using Code.Runtime.Services.GlobalRocket;
using Code.Runtime.Services.Library;
using Code.Runtime.Services.Player.CutsceneCopyProvider;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Infrastructure.DiInstallers.Library
{
    internal sealed class LibraryInstaller : MonoInstaller
    {
        [SerializeField]
        private Logic.Library _library;
        [SerializeField]
        private Rocket _rocket;
        [SerializeField]
        private GameObject _playerCutsceneCopy;
        
        private ILibraryService _libraryService;
        private IRocketProvider _rocketProvider;
        private IPlayerCutsceneCopyProvider _playerCutsceneCopyProvider;

        [Inject]
        private void Construct(ILibraryService libraryService, IRocketProvider rocketProvider, IPlayerCutsceneCopyProvider playerCutsceneCopyProvider)
        {
            _playerCutsceneCopyProvider = playerCutsceneCopyProvider;
            _rocketProvider = rocketProvider;
            _libraryService = libraryService;
        }

        public override void InstallBindings()
        {
            _libraryService.RegisterLibrary(_library);
            _rocketProvider.RegisterRocket(_rocket);
            _playerCutsceneCopyProvider.RegisterPlayer(_playerCutsceneCopy);
        }
    }
}