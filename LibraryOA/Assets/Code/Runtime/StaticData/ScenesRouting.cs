using UnityEngine;

namespace Code.Runtime.StaticData
{
    [CreateAssetMenu(fileName = "Scenes Routing", menuName = "Static data/Scenes Routing")]
    public class ScenesRouting : ScriptableObject
    {
        public string BootstrapScene;
        public string TargetScene;
        public string GameOverScene;
    }
}