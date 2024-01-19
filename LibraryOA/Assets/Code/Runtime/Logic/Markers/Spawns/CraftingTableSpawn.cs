using Code.Runtime.StaticData.Books;
using UnityEngine;

namespace Code.Runtime.Logic.Markers.Spawns
{
    [RequireComponent(typeof(UniqueId))]
    public sealed class CraftingTableSpawn : MonoBehaviour
    {
        [SerializeField] private StaticBook _initialBook;
    }
}