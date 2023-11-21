using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Logic.Markers.Spawns;
using Code.Runtime.Logic.Markers.Truck;
using Code.Runtime.StaticData;
using Code.Runtime.StaticData.SpawnersStaticData;
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
            List<BookSlotSpawnData> bookSlotsSpawns = FindObjectsOfType<BookSlotSpawn>()
                .Select(BookSlotSpawnData.NewFrom)
                .ToList();

            List<ReadingTableSpawnData> readingTableSpawns = FindObjectsOfType<ReadingTableSpawn>()
                .Select(ReadingTableSpawnData.NewFrom)
                .ToList();

            string sceneKey = SceneManager.GetActiveScene().name;
            Vector3 playerPosition = FindObjectOfType<PlayerInitialSpawn>().transform.position;

            TruckWay truckPathWay = FindObjectOfType<TruckWay>();
            TruckWayStaticData truckPathWayData = TruckWayStaticData.FromWayPoints(truckPathWay.LibraryPoint, truckPathWay.HiddenPoint);
            
            levelData.UpdateData(sceneKey, playerPosition, bookSlotsSpawns, readingTableSpawns, truckPathWayData);
            EditorUtility.SetDirty(levelData);
        }
    }
}