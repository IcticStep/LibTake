using Code.Runtime.Logic.GlobalGoals.RocketStart;
using Code.Runtime.Services.GlobalRocket;
using Code.Runtime.Services.Library;
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
        
        private ILibraryService _libraryService;
        private IRocketProvider _rocketProvider;

        [Inject]
        private void Construct(ILibraryService libraryService, IRocketProvider rocketProvider)
        {
            _rocketProvider = rocketProvider;
            _libraryService = libraryService;
        }

        public override void InstallBindings()
        {
            _libraryService.RegisterLibrary(_library);
            _rocketProvider.RegisterRocket(_rocket);
        }
    }
}