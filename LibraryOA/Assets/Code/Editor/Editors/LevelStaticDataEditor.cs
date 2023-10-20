using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Interactions;
using Code.Runtime.Logic.SpawnMarkers;
using Code.Runtime.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Editor.Editors
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                UpdateLevelData(levelData);
            }
        }

        private void UpdateLevelData(LevelStaticData levelData)
        {
            List<BookSlotSpawnData> bookSlotsSpawns = FindObjectsOfType<BookSlotSpawn>()
                .Select(x => new BookSlotSpawnData(x.GetComponent<UniqueId>().Id, x.GetComponent<BookSlotSpawn>().InitialBookId, x.transform.position))
                .ToList();
            string sceneKey = SceneManager.GetActiveScene().name;
            Vector3 playerPosition = FindObjectOfType<PlayerInitialSpawn>().transform.position;

            levelData.UpdateData(sceneKey, bookSlotsSpawns, playerPosition);
            EditorUtility.SetDirty(target);
        }
    }}