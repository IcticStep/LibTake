using UnityEngine;

namespace Code.Runtime.StaticData
{
    [CreateAssetMenu(fileName = "StartupSettings", menuName = "Static data/Startup settings")]
    public class StartupSettings : ScriptableObject
    {
        public string StartScene;
    }
}