#if DEVELOPMENT_BUILD
using Code.Runtime.StaticData.DebugTools;
using UnityEngine;
#endif

namespace Code.Runtime.Infrastructure.DebugToolsService
{
    internal sealed class DebugToolsInitializer
    {
#if DEVELOPMENT_BUILD
        private const string StaticDataPath = "Static Data/DebugTools/Debug tools data";
        private DebugToolsData _staticData;

        public void Initialize()
        {
            _staticData = Resources.Load<DebugToolsData>(StaticDataPath);
            SetUpConsole();
            SetUpProfiling();
        }

        private void SetUpConsole() =>
            Object.Instantiate(_staticData.ConsolePrefab);

        private void SetUpProfiling() =>
            Object.Instantiate(_staticData.ProfilerPrefab);
#endif
    }
}