using JetBrains.Annotations;

namespace Code.Runtime.Services.Library
{
    [UsedImplicitly]
    internal sealed class LibraryService : ILibraryService
    {
        private Logic.Library _library;
        
        public void RegisterLibrary(Logic.Library library) =>
            _library = library;
        
        public void CleanUp() =>
            _library = null;

        public void ShowSecondFloor() =>
            _library.ShowSecondFloor();
        
        public void HideSecondFloor() =>
            _library.HideSecondFloor();
    }
}