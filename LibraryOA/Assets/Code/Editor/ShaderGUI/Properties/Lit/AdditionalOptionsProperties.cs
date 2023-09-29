using UnityEditor;

namespace Code.Editor.ShaderGUI.Properties.Lit
{
    public struct AdditionalOptionsProperties
    {
        public MaterialProperty SpecularHighlights;
        public MaterialProperty EnvironmentReflections;
        
        public AdditionalOptionsProperties(MaterialProperty[] properties)
        {
            SpecularHighlights = BaseShaderGUI.FindProperty("_SpecularHighlights", properties, false);
            EnvironmentReflections = BaseShaderGUI.FindProperty("_EnvironmentReflections", properties, false);
        }
    }
}