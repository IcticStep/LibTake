using UnityEditor;
using UnityEngine;
using Code.Editor.ShaderGUI.Properties.Lit;

namespace Code.Editor.ShaderGUI
{
    public class LitGUI : BaseShaderGUI
    {
        private InputsProperties _inputs;
        private AdditionalOptionsProperties _additionalOptionsProperties;
        private SetUpKeywords _setUpKeywords;
        
        public override void FindProperties(MaterialProperty[] properties)
        {
            base.FindProperties(properties); 
            _inputs = new InputsProperties(properties);
            _additionalOptionsProperties = new AdditionalOptionsProperties(properties);
        }
    
        public override void DrawSurfaceOptions(Material material)
        {
            EditorGUIUtility.labelWidth = 0f;
            base.DrawSurfaceOptions(material);
        }
        
        void SetLitKeywords(Material material) => 
            _setUpKeywords = new SetUpKeywords(material);

        public override void ValidateMaterial(Material material)
        {
            SetMaterialKeywords(material, SetLitKeywords);
        }

        public override void DrawSurfaceInputs(Material material)
        {
            base.DrawSurfaceInputs(material);
            materialEditor.ShaderProperty(_inputs.Smoothness, new GUIContent("Smoothness"));
            materialEditor.TexturePropertySingleLine(new GUIContent("Specular Map"),
                _inputs.SpecularColorMap, _inputs.SpecularColor);
            DrawNormalArea(materialEditor, _inputs.NormalMap, _inputs.NormalScale);
            
            DrawEmissionProperties(material, true);
            FloatToggle.DrawFloatToggle(new GUIContent("Emission Fresnel"), _inputs.EnableEmissionFresnel);
            materialEditor.ShaderProperty(_inputs.EmissionFresnelPower, new GUIContent("Emission Fresnel Power"));
                
            DrawTileOffset(materialEditor, baseMapProp);
        }

        public override void DrawAdvancedOptions(Material material)
        {
            FloatToggle.DrawFloatToggle(new GUIContent("Specular Highlights"), _additionalOptionsProperties.SpecularHighlights);
            FloatToggle.DrawFloatToggle(new GUIContent("Environment Reflections"), _additionalOptionsProperties.EnvironmentReflections);
            
            base.DrawAdvancedOptions(material);
        }
    }
}
