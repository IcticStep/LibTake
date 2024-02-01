using UnityEngine;

namespace Code.Runtime.StaticData
{
    [CreateAssetMenu(fileName = "Scenes Routing", menuName = "Static data/Scenes Routing")]
    public class ScenesRouting : ScriptableObject
    {
        public string BootstrapScene;
        public string MenuScene;
        public string LevelScene;
        public string GameOverScene;
        public string AuthorsScene;
    }
}