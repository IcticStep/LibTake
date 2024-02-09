using Code.Runtime.Services.Library;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Infrastructure.DiInstallers.Library
{
    internal sealed class LibraryInstaller : MonoInstaller
    {
        [SerializeField]
        private Logic.Library _library;
        
        private ILibraryService _libraryService;

        [Inject]
        private void Construct(ILibraryService libraryService) =>
            _libraryService = libraryService;

        public override void InstallBindings() =>
            _libraryService.RegisterLibrary(_library);
    }
}