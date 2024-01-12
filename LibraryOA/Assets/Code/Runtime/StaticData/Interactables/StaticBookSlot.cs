using UnityEngine;

namespace Code.Runtime.StaticData.Interactables
{
    [CreateAssetMenu(fileName = "Book slot data", menuName = "Static data/Interactables/Book Slot", order = 0)]
    public class StaticBookSlot : ScriptableObject
    {
        [field: SerializeField]
        public GameObject Prefab { get; private set; }
    }
}