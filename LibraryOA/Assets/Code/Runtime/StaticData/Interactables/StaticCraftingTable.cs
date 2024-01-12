using UnityEngine;

namespace Code.Runtime.StaticData.Interactables
{
    [CreateAssetMenu(fileName = "Crafting data", menuName = "Static data/Interactables/Crafting table", order = 0)]
    public class StaticCraftingTable : ScriptableObject
    {
        [field: SerializeField]
        public GameObject Prefab { get; private set; }
    }
}