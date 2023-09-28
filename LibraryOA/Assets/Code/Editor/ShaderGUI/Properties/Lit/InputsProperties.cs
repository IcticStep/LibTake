using UnityEditor;
using UnityEngine;

namespace Code.Editor.ShaderGUI.Properties.Lit
{
    public struct InputsProperties
    {
        public MaterialProperty SpecularColor;
        public MaterialProperty SpecularColorMap;
        public MaterialProperty Smoothness;
        
        public MaterialProperty NormalMap;
        public MaterialProperty NormalScale;
        
        public MaterialProperty EnableEmissionFresnel;
        public MaterialProperty EmissionFresnelPower;
        
        public static readonly int ColorID = Shader.PropertyToID("_Color");
        public static readonly int MainTexID = Shader.PropertyToID("_MainTex");
        public static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
        public static readonly int BaseMapID = Shader.PropertyToID("_BaseMap");
        public static readonly int BumpMapID = Shader.PropertyToID("_BumpMap");
        public static readonly int EmissionEnabledID = Shader.PropertyToID("_EmissionEnabled");
        public static readonly int EnableEmissionFresnelID = Shader.PropertyToID("_EnableEmissionFresnel");
        public static readonly int SpecularHighlightsID = Shader.PropertyToID("_SpecularHighlights");
        public static readonly int EnvironmentReflectionsID = Shader.PropertyToID("_EnvironmentReflections");
        
        public InputsProperties(MaterialProperty[] properties)
        {
            SpecularColor = BaseShaderGUI.FindProperty("_SpecularColor", properties, false);
            SpecularColorMap = BaseShaderGUI.FindProperty("_SpecularColorMap", properties, false);
            Smoothness = BaseShaderGUI.FindProperty("_Smoothness", properties, false);
            
            NormalMap = BaseShaderGUI.FindProperty("_BumpMap", properties, false);
            NormalScale = BaseShaderGUI.FindProperty("_BumpScale", properties, false);
            
            EnableEmissionFresnel = BaseShaderGUI.FindProperty("_EnableEmissionFresnel", properties, false);
            EmissionFresnelPower = BaseShaderGUI.FindProperty("_EmissionFresnelPower", properties, false);
        }
    }
}