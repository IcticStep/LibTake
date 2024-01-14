using System.Linq;
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
            GlobalStep previousStep = null;
            foreach(GlobalStep globalStep in globalGoal.GlobalSteps)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField(globalStep.Name);

                EditorGUI.indentLevel++;
                DrawGlobalStepSkillRequirements(globalStep, previousStep);
                previousStep = globalStep;

                EditorGUI.indentLevel -= 2;
            }
        }

        private void DrawGlobalStepSkillRequirements(GlobalStep globalStep, GlobalStep previousStep)
        {
            EditorGUILayout.BeginHorizontal();
            
            EditorGUIUtility.labelWidth = 110;
            
            foreach(SkillConstraint skillRequirement in globalStep.SkillRequirements)
            {
                EditorGUILayout.BeginVertical();

                int delta = GetDelta(previousStep, skillRequirement);
                string deltaDisplay = delta > 0 ? $"(+{delta})" : string.Empty;
                EditorGUILayout.LabelField(skillRequirement.BookType.ToString(), $"{skillRequirement.RequiredLevel} {deltaDisplay}");
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.Space(7);
            
            EditorGUILayout.EndHorizontal();
        }

        private static int GetDelta(GlobalStep previousStep, SkillConstraint skillRequirement)
        {
            if(previousStep is null)
                return 0;
            
            SkillConstraint previousSkillRequirement = previousStep.SkillRequirements.First(skill => skill.BookType == skillRequirement.BookType);
            int delta = skillRequirement.RequiredLevel - previousSkillRequirement.RequiredLevel;
            delta = Mathf.Max(delta, 0);
            return delta;
        }
    }
}