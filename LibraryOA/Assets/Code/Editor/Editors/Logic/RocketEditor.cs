using Code.Runtime.Logic.GlobalGoals;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.Logic
{
    [CustomEditor(typeof(Rocket))]
    public class RocketEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Launch"))
            {
                if (!Application.isPlaying)
                {
                    Debug.LogError("You can't launch rocket in edit mode");
                    return;
                }
                
                Rocket rocket = (Rocket)target;
                rocket.Launch().Forget();
            }
        }
    }
}