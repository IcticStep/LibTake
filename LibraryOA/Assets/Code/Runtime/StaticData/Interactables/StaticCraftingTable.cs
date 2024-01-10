using UnityEngine;

namespace Code.Runtime.StaticData.Interactables
{
    [CreateAssetMenu(fileName = "Crafting Table Data", menuName = "Static data/Crafting table data", order = 0)]
    public class StaticCraftingTable : ScriptableObject
    {
        [field: SerializeField]
        public GameObject Prefab { get; private set; }
    }
}