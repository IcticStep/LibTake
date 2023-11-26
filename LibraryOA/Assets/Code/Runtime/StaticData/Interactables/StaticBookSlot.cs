using UnityEngine;

namespace Code.Runtime.StaticData.Interactables
{
    [CreateAssetMenu(fileName = "BookSlotData", menuName = "Static data/BookSlotData", order = 0)]
    public class StaticBookSlot : ScriptableObject
    {
        [field: SerializeField]
        public GameObject Prefab { get; private set; }
    }
}