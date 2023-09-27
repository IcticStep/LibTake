
#ifndef GLOBAL_ILLUMINATION_INCLUDED
#define GLOBAL_ILLUMINATION_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/GlobalIllumination.hlsl"

half3 LitGlobalIllumination(BRDFData brdfData, half3 bakedGI, half occlusion, float3 positionWS, 
                            half3 normalWS, half3 viewDirectionWS, float2 normalizedScreenSpaceUV)
{
    half3 reflectVector = reflect(-viewDirectionWS, normalWS);
    half NoV = saturate(dot(normalWS, viewDirectionWS));
    half fresnelTerm = Pow4(1.0 - NoV);

    half3 indirectDiffuse = bakedGI;
    half3 indirectSpecular = GlossyEnvironmentReflection(reflectVector, positionWS, brdfData.perceptualRoughness, 1.0, normalizedScreenSpaceUV);

    half3 color = EnvironmentBRDF(brdfData, indirectDiffuse, indirectSpecular, fresnelTerm);

    if (IsOnlyAOLightingFeatureEnabled())
    {
        color = half3(1, 1, 1); // "Base white" for AO debug lighting mode
    }

    return color * occlusion;
}

#if !USE_FORWARD_PLUS
half3 LitGlobalIllumination(BRDFData brdfData, half3 bakedGI, half occlusion, 
                            float3 positionWS, half3 normalWS, half3 viewDirectionWS)
{
    return LitGlobalIllumination(brdfData, bakedGI, occlusion, positionWS, normalWS, viewDirectionWS, float2(0.0, 0.0));
}
#endif

half3 LitGlobalIllumination(BRDFData brdfData, half3 bakedGI, half occlusion, half3 normalWS, 
                            half3 viewDirectionWS, float2 normalizedScreenSpaceUV)
{
    half3 reflectVector = reflect(-viewDirectionWS, normalWS);
    half NoV = saturate(dot(normalWS, viewDirectionWS));
    half fresnelTerm = Pow4(1.0 - NoV);

    half3 indirectDiffuse = bakedGI;
    half3 indirectSpecular = GlossyEnvironmentReflection(reflectVector, brdfData.perceptualRoughness, 1.0);

    half3 color = EnvironmentBRDF(brdfData, indirectDiffuse, indirectSpecular, fresnelTerm);

    if (IsOnlyAOLightingFeatureEnabled())
    {
        color = half3(1, 1, 1); // "Base white" for AO debug lighting mode
    }

    return color * occlusion;
}

#endif
