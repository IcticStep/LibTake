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
                DrawStep(globalStep, previousStep);
                previousStep = globalStep;
            }
        }

        private void DrawStep(GlobalStep globalStep, GlobalStep previousStep)
        {
            EditorGUI.indentLevel++;
            int delta = GetDeltaForStep(globalStep, previousStep);
            string displayDelta = GetDeltaDisplay(delta);
            
            EditorGUILayout.LabelField($"{globalStep.Name} {displayDelta}");

            EditorGUI.indentLevel++;
            DrawGlobalStepSkillRequirements(globalStep, previousStep);

            EditorGUI.indentLevel -= 2;
        }

        private void DrawGlobalStepSkillRequirements(GlobalStep globalStep, GlobalStep previousStep)
        {
            EditorGUILayout.BeginHorizontal();
            
            EditorGUIUtility.labelWidth = 110;
            
            foreach(SkillConstraint skillRequirement in globalStep.SkillRequirements)
            {
                EditorGUILayout.BeginVertical();

                int delta = GetDeltaForSkill(previousStep, skillRequirement);
                string deltaDisplay = GetDeltaDisplay(delta);
                EditorGUILayout.LabelField(skillRequirement.BookType.ToString(), $"{skillRequirement.RequiredLevel} {deltaDisplay}");
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.Space(7);
            
            EditorGUILayout.EndHorizontal();
        }

        private static int GetDeltaForSkill(GlobalStep previousStep, SkillConstraint skillRequirement)
        {
            if(previousStep is null)
                return 0;
            
            SkillConstraint previousSkillRequirement = previousStep.SkillRequirements.First(skill => skill.BookType == skillRequirement.BookType);
            int delta = skillRequirement.RequiredLevel - previousSkillRequirement.RequiredLevel;
            delta = Mathf.Max(delta, 0);
            return delta;
        }

        private static int GetDeltaForStep(GlobalStep step, GlobalStep previousStep) =>
            step
                .SkillRequirements
                .Sum(skillRequirement => 
                    previousStep is null 
                        ? skillRequirement.RequiredLevel 
                        : GetDeltaForSkill(previousStep, skillRequirement));

        private static string GetDeltaDisplay(int delta) =>
            delta > 0 ? $"(+{delta})" : string.Empty;
    }
}