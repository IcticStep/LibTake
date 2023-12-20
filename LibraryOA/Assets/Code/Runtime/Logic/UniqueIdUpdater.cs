using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Runtime.Logic
{
    public sealed class UniqueIdUpdater
    {
        public void UpdateUniqueId(UniqueId uniqueId)
        {
            if(IsPrefab(uniqueId))
                return;

            if(HasNoId(uniqueId) || HasPrefabId(uniqueId))
            {
                Generate(uniqueId);
                return;
            }

            if(IsUnique(uniqueId))
                return;

            Generate(uniqueId);
        }

        public void ForceUpdateUniqueId(UniqueId uniqueId) =>
            Generate(uniqueId);

        private static bool HasPrefabId(UniqueId uniqueId) =>
            uniqueId.Id[0] == '_';

        private static bool HasNoId(UniqueId uniqueId) =>
            string.IsNullOrEmpty(uniqueId.Id);

        private bool IsPrefab(UniqueId uniqueId) =>
            uniqueId.gameObject.scene.rootCount == 0;

        private bool IsUnique(UniqueId uniqueId)
        {
            UniqueId[] uniqueIds = Object.FindObjectsOfType<UniqueId>();
            return !uniqueIds.Any(other => other != uniqueId && other.Id == uniqueId.Id);
        }

        private void Generate(UniqueId uniqueId)
        {
            string id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";
            uniqueId.InitId(id);

#if UNITY_EDITOR
            if(Application.isPlaying)
                return;

            UnityEditor.EditorUtility.SetDirty(uniqueId);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
#endif
        }
    }
}