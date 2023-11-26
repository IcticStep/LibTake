using Code.Runtime.StaticData.Interactables;
using Code.Runtime.StaticData.Level.MarkersStaticData;
using Code.Runtime.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.StaticData.Level
{
    [CreateAssetMenu(fileName = "Level", menuName = "Static data/Level")]
    public sealed class LevelStaticData : ScriptableObject
    {
        [field: ReadOnly]
        [field: SerializeField]
        public string LevelKey { get; private set; }

        [field: ReadOnly]
        [field: SerializeField]
        public Vector3 PlayerInitialPosition { get; private set; }

        [field: SerializeField]
        public InteractablesData Interactables { get; private set; }

        [FormerlySerializedAs("CustomersData")]
        [field: SerializeField]
        public CustomersData Customers;

        [field: SerializeField]
        public TruckWayStaticData TruckWay { get; private set; }

        public void UpdateData(
            string levelKey, 
            Vector3 playerInitialPosition,
            CustomersData customersData, 
            InteractablesData interactablesData,
            TruckWayStaticData wayStaticData)
        {
            LevelKey = levelKey;
            PlayerInitialPosition = playerInitialPosition;
            Customers = customersData;
            Interactables = interactablesData;
            TruckWay = wayStaticData;
        }
    }
}