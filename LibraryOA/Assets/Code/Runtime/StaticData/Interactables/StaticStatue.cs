using UnityEngine;

namespace Code.Runtime.StaticData.Interactables
{
    [CreateAssetMenu(fileName = "Statue data", menuName = "Static data/Interactables/Statue", order = 0)]
    public class StaticStatue : ScriptableObject
    {
        [field: SerializeField]
        public GameObject Prefab { get; private set; }
        
        [field: SerializeField]
        public int LifeRestorePrice { get; private set; }
    }
}