using UnityEngine;

namespace Code.Runtime.Logic.SpawnMarkers
{
    public sealed class BookSlotSpawnContainer : MonoBehaviour
    {
        [field: SerializeField]
        public float CircleRadius { get; set; } = 1;

        [field: SerializeField]
        public int ObjectsToSkip { get; set; } = 0;
    }
}