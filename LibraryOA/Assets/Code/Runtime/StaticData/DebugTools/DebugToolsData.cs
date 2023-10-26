using UnityEngine;

namespace Code.Runtime.StaticData.DebugTools
{
    [CreateAssetMenu(fileName = "Debug tools data", menuName = "Static data/Debug tools data")]
    internal sealed class DebugToolsData : ScriptableObject
    {
        [field: SerializeField]
        public GameObject ConsolePrefab;
    }
}