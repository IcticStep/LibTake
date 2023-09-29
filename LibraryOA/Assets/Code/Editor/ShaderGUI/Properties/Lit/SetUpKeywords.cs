using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Code.Editor.ShaderGUI.Properties.Lit
{
    public class SetUpKeywords
    {
        public SetUpKeywords(Material material)
        {
            SetInputsKeywords(material);
            SetAdditionalOptionsKeywords(material);
        }
        
        public void SetInputsKeywords(Material material)
        {
            if (material.HasProperty(InputsProperties.MainTexID) && material.HasProperty(InputsProperties.BaseMapID))
            {
                material.SetTexture(InputsProperties.MainTexID, material.GetTexture(InputsProperties.BaseMapID));
                material.SetTextureScale(InputsProperties.MainTexID, material.GetTextureScale(InputsProperties.BaseMapID));
                material.SetTextureOffset(InputsProperties.MainTexID, material.GetTextureOffset(InputsProperties.BaseMapID));
            }
            if (material.HasProperty(InputsProperties.ColorID) && material.HasProperty(InputsProperties.BaseColorID))
                material.SetColor(InputsProperties.ColorID, material.GetColor(InputsProperties.BaseColorID));
            
            if (material.HasProperty(InputsProperties.BumpMapID))
                CoreUtils.SetKeyword(material, Keywords.kNormalMap, material.GetTexture(InputsProperties.BumpMapID));
            
            SetEmissionKeywords(material);
        }

        public void SetEmissionKeywords(Material material)
        {
            if (material.HasProperty("_EmissionColor"))
                MaterialEditor.FixupEmissiveFlag(material);

            var shouldEmissionBeEnabled =
                (material.globalIlluminationFlags & MaterialGlobalIlluminationFlags.EmissiveIsBlack) == 0;

            if (material.HasProperty(InputsProperties.EmissionEnabledID) && !shouldEmissionBeEnabled)
                shouldEmissionBeEnabled = material.GetFloat(InputsProperties.EmissionEnabledID) > 0.5f;

            CoreUtils.SetKeyword(material, Keywords.kEmission, shouldEmissionBeEnabled);

            if (material.HasProperty(InputsProperties.EnableEmissionFresnelID))
            {
                var emissionFresnelState = material.GetFloat(InputsProperties.EnableEmissionFresnelID) > 0.5f;
                CoreUtils.SetKeyword(material, Keywords.kEmissionFresnel, emissionFresnelState);
            }
        }

        public void SetAdditionalOptionsKeywords(Material material)
        {
            if (material.HasProperty(InputsProperties.SpecularHighlightsID))
                CoreUtils.SetKeyword(material, Keywords.kSpecularHighlightsOff, 
                    material.GetFloat(InputsProperties.SpecularHighlightsID) < 0.5f);

            if (material.HasProperty(InputsProperties.EnvironmentReflectionsID))
                CoreUtils.SetKeyword(material, Keywords.kEnvironmentReflectionsOff, 
                    material.GetFloat(InputsProperties.EnvironmentReflectionsID) < 0.5f);
        }
    }
}