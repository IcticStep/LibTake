using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Logic.Customers;
using Code.Runtime.Logic.Markers.Customers;
using Code.Runtime.Logic.Markers.Spawns;
using Code.Runtime.Logic.Markers.Truck;
using Code.Runtime.StaticData;
using Code.Runtime.StaticData.Interactables;
using Code.Runtime.StaticData.Level;
using Code.Runtime.StaticData.Level.MarkersStaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Editor.Editors.StaticData
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if(GUILayout.Button("Collect"))
            {
                UpdateLevelData(levelData);
            }
        }

        public static void UpdateLevelData(LevelStaticData levelData)
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            Vector3 playerPosition = FindObjectOfType<PlayerInitialSpawn>().transform.position;
            
            InteractablesData interactablesData = CollectInteractablesData();
            CustomersData customersData = CollectCustomersData();
            TruckWayStaticData truckPathWayData = CollectTruckData();

            levelData.UpdateData(sceneKey, playerPosition, customersData, interactablesData, truckPathWayData);
            EditorUtility.SetDirty(levelData);
        }

        private static CustomersData CollectCustomersData()
        {
            Vector3 spawnPoint = FindObjectOfType<CustomersSpawnPoint>().transform.position;
            List<Vector3> queuePoints = FindObjectOfType<CustomersQueuePointsContainer>()
                .Points
                .Select(x => x.transform.position)
                .ToList();

            List<Vector3> exitWayPoints = FindObjectOfType<CustomersWayContainer>()
                .WayPoints
                .Select(x => x.transform.position)
                .ToList();

            CustomersData customersData = new(spawnPoint, queuePoints, exitWayPoints);
            return customersData;
        }

        private static InteractablesData CollectInteractablesData()
        {
            List<BookSlotSpawnData> bookSlotsSpawns = FindObjectsOfType<BookSlotSpawn>()
                .Select(BookSlotSpawnData.NewFrom)
                .ToList();

            List<ReadingTableSpawnData> readingTableSpawns = FindObjectsOfType<ReadingTableSpawn>()
                .Select(ReadingTableSpawnData.NewFrom)
                .ToList();

            return new(bookSlotsSpawns, readingTableSpawns);
        }

        private static TruckWayStaticData CollectTruckData()
        {
            TruckWay truckPathWay = FindObjectOfType<TruckWay>();
            TruckWayStaticData truckPathWayData = TruckWayStaticData.FromWayPoints(truckPathWay.LibraryPoint, truckPathWay.HiddenPoint);
            return truckPathWayData;
        }
    }
}