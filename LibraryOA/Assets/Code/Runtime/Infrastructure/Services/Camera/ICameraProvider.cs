namespace Code.Runtime.Infrastructure.Services.Camera
{
    internal interface ICameraProvider
    {
        UnityEngine.Camera MainCamera { get; }
        void Initialize(UnityEngine.Camera main);
        void CleanUp();
    }
}