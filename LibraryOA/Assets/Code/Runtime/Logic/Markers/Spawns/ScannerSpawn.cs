using Code.Runtime.StaticData.Books;
using UnityEngine;

namespace Code.Runtime.Logic.Markers.Spawns
{
    [RequireComponent(typeof(UniqueId))]
    public sealed class ScannerSpawn : MonoBehaviour
    {
        [SerializeField] private StaticBook _initialBook;
        
        // ReSharper disable once Unity.NoNullPropagation
        public string InitialBookId => _initialBook?.Id;
    }
}