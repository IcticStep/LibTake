using UnityEngine;

namespace Code.Runtime.StaticData
{
    [CreateAssetMenu(fileName = "Player", menuName = "Static data/Player", order = 0)]
    public class StaticPlayer : ScriptableObject
    {
        [field: SerializeField]
        public GameObject Prefab { get; private set; }
        
        [field: SerializeField]
        public int StartLivesCount { get; private set; }
    }
}