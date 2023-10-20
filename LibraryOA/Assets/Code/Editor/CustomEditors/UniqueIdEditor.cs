using System;
using System.Linq;
using Code.Runtime.Logic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Code.Editor.CustomEditors
{
    [CustomEditor(typeof(UniqueId))]
    internal sealed class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            UniqueId uniqueId = (UniqueId) target;
      
            if(IsPrefab(uniqueId))
                return;

            if(HasNoId(uniqueId) || HasPrefabId(uniqueId))
            {
                Generate(uniqueId);
                return;
            }

            if(IsIsUnique(uniqueId))
                return;

            Generate(uniqueId);
        }

        private static bool HasPrefabId(UniqueId uniqueId) =>
            uniqueId.Id[0] == '_';

        private static bool HasNoId(UniqueId uniqueId) =>
            string.IsNullOrEmpty(uniqueId.Id);

        private bool IsPrefab(UniqueId uniqueId) => 
            uniqueId.gameObject.scene.rootCount == 0;

        private bool IsIsUnique(UniqueId uniqueId)
        {
            UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();
            return !uniqueIds.Any(other => other != uniqueId && other.Id == uniqueId.Id);
        }

        private void Generate(UniqueId uniqueId)
        {
            string id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";
            uniqueId.InitId(id);

            if(Application.isPlaying)
                return;

            EditorUtility.SetDirty(uniqueId);
            EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
        }
    }
}