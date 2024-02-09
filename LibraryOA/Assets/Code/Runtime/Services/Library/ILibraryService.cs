namespace Code.Runtime.Services.Library
{
    internal interface ILibraryService
    {
        void RegisterLibrary(Logic.Library library);
        void CleanUp();
        void ShowSecondFloor();
        void HideSecondFloor();
    }
}