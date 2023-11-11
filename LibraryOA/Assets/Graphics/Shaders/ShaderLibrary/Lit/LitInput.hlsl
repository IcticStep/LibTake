#ifndef LIT_INPUT_INCLUDED
#define LIT_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"
#include "ShaderLibrary/SurfaceInput.hlsl"
#include "ShaderLibrary/Lit/LitMaps.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"

// NOTE: Do not ifdef the properties here as SRP batcher can not handle different layouts.
CBUFFER_START(UnityPerMaterial)
half _Surface;
half _Cutoff;

float4 _BaseMap_ST;
half4 _BaseColor;
CBUFFER_END

// NOTE: Do not ifdef the properties for dots instancing, but ifdef the actual usage.
// Otherwise you might break CPU-side as property constant-buffer offsets change per variant.
// NOTE: Dots instancing is orthogonal to the constant buffer above.
#ifdef UNITY_DOTS_INSTANCING_ENABLED

UNITY_DOTS_INSTANCING_START(MaterialPropertyMetadata)
    UNITY_DOTS_INSTANCED_PROP(float , _Surface)
    UNITY_DOTS_INSTANCED_PROP(float , _Cutoff)

    UNITY_DOTS_INSTANCED_PROP(float4, _BaseColor)
UNITY_DOTS_INSTANCING_END(MaterialPropertyMetadata)

#define _Surface                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _Surface)
#define _Cutoff                 UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _Cutoff)

#define _BaseColor              UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4 , _BaseColor)
#endif

inline void InitializeStandardLitSurfaceData(float2 uv, out SurfaceData outSurfaceData)
{
    half4 albedoAlpha = SampleAlbedoAlpha(uv, TEXTURE2D_ARGS(_BaseMap, sampler_BaseMap));
    outSurfaceData.alpha = Alpha(albedoAlpha.a, _BaseColor, _Cutoff);

    outSurfaceData.albedo = albedoAlpha.rgb * _BaseColor.rgb;
    outSurfaceData.albedo = AlphaModulate(outSurfaceData.albedo, outSurfaceData.alpha);

    outSurfaceData.metallic = half(1.0);
    outSurfaceData.specular = 0.0h;

    outSurfaceData.smoothness = 0.0h;
    outSurfaceData.normalTS = half3(0.0, 0.0, 1.0);
    outSurfaceData.occlusion = 1.0h;
    outSurfaceData.emission = 0.0;

    outSurfaceData.clearCoatMask = half(0.0);
    outSurfaceData.clearCoatSmoothness = half(0.0);
}

#endif