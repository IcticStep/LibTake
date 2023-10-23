using Code.Runtime.StaticData;
using UnityEngine;

namespace Code.Runtime.Logic.SpawnMarkers
{
    [RequireComponent(typeof(UniqueId))]
    public sealed class ReadingTableSpawn : MonoBehaviour
    {
        [SerializeField] private StaticBook _initialBook;
        
        // ReSharper disable once Unity.NoNullPropagation
        public string InitialBookId => _initialBook?.Id;
    }
}