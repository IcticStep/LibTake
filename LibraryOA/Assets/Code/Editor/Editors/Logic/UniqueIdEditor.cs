using Code.Runtime.Logic;
using UnityEditor;

namespace Code.Editor.Editors.Logic
{
    [CustomEditor(typeof(UniqueId))]
    internal sealed class UniqueIdEditor : UnityEditor.Editor
    {
        private readonly UniqueIdUpdater _uniqueIdUpdater = new();
        
        private void OnEnable() =>
            _uniqueIdUpdater.UpdateUniqueId((UniqueId)target);
    }
}