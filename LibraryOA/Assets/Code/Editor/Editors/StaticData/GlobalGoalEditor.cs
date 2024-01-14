using Code.Runtime.StaticData.GlobalGoals;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.StaticData
{
    [CustomEditor(typeof(GlobalGoal))]
    internal sealed class GlobalGoalEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DrawSkillRequirementsForSteps();
        }

        private void DrawSkillRequirementsForSteps()
        {
            GlobalGoal globalGoal = (GlobalGoal)target;
            
            GUILayout.Label("Skills requirements");

            using(new EditorGUI.DisabledScope(true))
            {
                foreach(GlobalStep globalStep in globalGoal.GlobalSteps)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.LabelField(globalStep.Name);
                
                    EditorGUI.indentLevel++;
                    DrawGlobalStepSkillRequirements(globalStep);

                    EditorGUI.indentLevel-=2;
                }
            }
        }

        private void DrawGlobalStepSkillRequirements(GlobalStep globalStep)
        {
            EditorGUILayout.BeginHorizontal();
            
            EditorGUIUtility.labelWidth = 110;

            foreach(SkillConstraint skillRequirement in globalStep.SkillRequirements)
                EditorGUILayout.FloatField(skillRequirement.BookType.ToString(), skillRequirement.RequiredLevel);
            EditorGUILayout.Space(7);
            
            EditorGUILayout.EndHorizontal();
        }
    }
}