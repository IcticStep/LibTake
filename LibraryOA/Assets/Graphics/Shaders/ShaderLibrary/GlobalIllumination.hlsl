
#ifndef UNIVERSAL_GLOBAL_ILLUMINATION_INCLUDED
#define UNIVERSAL_GLOBAL_ILLUMINATION_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/GlobalIllumination.hlsl"

half3 LitGlobalIllumination(SurfaceData surfaceData, BRDFData brdfData, VectorsData vData,
                                float3 positionWS, half3 bakedGI, float2 normalizedScreenSpaceUV, half occlusion)
{
    half3 reflectVector = reflect(-vData.viewDirectionWS, vData.normalWS);
    half NoV = saturate(dot(vData.normalWS, vData.viewDirectionWS));
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
half3 LitGlobalIllumination(SurfaceData surfaceData, BRDFData brdfData, BRDFData brdfDataCoat, VectorsData vData,
                                float3 positionWS, half3 bakedGI, half occlusion)
{
    return LitGlobalIllumination(surfaceData, brdfData, brdfDataCoat, vData, positionWS, bakedGI, float2(0.0, 0.0), occlusion);
}
#endif

// Backwards compatiblity
half3 LitGlobalIllumination(SurfaceData surfaceData, BRDFData brdfData, VectorsData vData, float3 positionWS, half3 bakedGI, float2 normalizedScreenSpaceUV, half occlusion)
{
    const BRDFData noClearCoat = (BRDFData)0;
    return LitGlobalIllumination(surfaceData, brdfData, noClearCoat, vData, positionWS, bakedGI,
                                     normalizedScreenSpaceUV, occlusion);
}

half3 LitGlobalIllumination(SurfaceData surfaceData, BRDFData brdfData, VectorsData vData, float3 positionWS, half3 bakedGI, half occlusion)
{
    const BRDFData noClearCoat = (BRDFData)0;
    return LitGlobalIllumination(surfaceData, brdfData, noClearCoat, vData, positionWS, bakedGI, 0.0, occlusion);
}

half3 LitGlobalIllumination(SurfaceData surfaceData, BRDFData brdfData, VectorsData vData, half3 bakedGI, half occlusion)
{
    half3 reflectVector = AnisotropyReflectVector(vData, brdfData.perceptualRoughness, surfaceData.anisotropy);
    half NoV = saturate(dot(vData.normalWS, vData.viewDirectionWS));
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
