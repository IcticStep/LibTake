#ifndef LIT_INPUT_INCLUDED
#define LIT_INPUT_INCLUDED

#define _EMISSION

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
half4 _SpecularColor;
half _Smoothness;
half _NormalScale;

half4 _EmissionColor;
CBUFFER_END

// NOTE: Do not ifdef the properties for dots instancing, but ifdef the actual usage.
// Otherwise you might break CPU-side as property constant-buffer offsets change per variant.
// NOTE: Dots instancing is orthogonal to the constant buffer above.
#ifdef UNITY_DOTS_INSTANCING_ENABLED

UNITY_DOTS_INSTANCING_START(MaterialPropertyMetadata)
    UNITY_DOTS_INSTANCED_PROP(float , _Surface)
    UNITY_DOTS_INSTANCED_PROP(float , _Cutoff)

    UNITY_DOTS_INSTANCED_PROP(float4, _BaseColor)
    UNITY_DOTS_INSTANCED_PROP(float4, _SpecularColor)
    UNITY_DOTS_INSTANCED_PROP(float , _Smoothness)
    UNITY_DOTS_INSTANCED_PROP(float , _Metallic)
    UNITY_DOTS_INSTANCED_PROP(float , _NormalScale)

    UNITY_DOTS_INSTANCED_PROP(float4, _EmissionColor)
UNITY_DOTS_INSTANCING_END(MaterialPropertyMetadata)

#define _Surface                UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _Surface)
#define _Cutoff                 UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _Cutoff)

#define _BaseColor              UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4 , _BaseColor)
#define _SpecularColor          UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4 , _SpecularColor)
#define _Smoothness             UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _Smoothness)
#define _NormalScale            UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float  , _NormalScale)

#define _EmissionColor          UNITY_ACCESS_DOTS_INSTANCED_PROP_WITH_DEFAULT(float4 , _EmissionColor)
#endif

inline void InitializeStandardLitSurfaceData(float2 uv, out SurfaceData outSurfaceData)
{
    half4 albedoAlpha = SampleAlbedoAlpha(uv, TEXTURE2D_ARGS(_BaseMap, sampler_BaseMap));
    outSurfaceData.alpha = Alpha(albedoAlpha.a, _BaseColor, _Cutoff);

    outSurfaceData.albedo = albedoAlpha.rgb * _BaseColor.rgb;
    outSurfaceData.albedo = AlphaModulate(outSurfaceData.albedo, outSurfaceData.alpha);

    outSurfaceData.metallic = 0.0h;
    outSurfaceData.specular = _SpecularColor.rgb;

    outSurfaceData.smoothness = _Smoothness;
    outSurfaceData.normalTS = SampleNormal(uv, TEXTURE2D_ARGS(_NormalMap, sampler_NormalMap), _NormalScale);
    outSurfaceData.occlusion = 1.0h;
    outSurfaceData.emission = SampleEmission(uv, _EmissionColor.rgb, TEXTURE2D_ARGS(_EmissionMap, sampler_EmissionMap));

    outSurfaceData.clearCoatMask = half(0.0);
    outSurfaceData.clearCoatSmoothness = half(0.0);
}

#endif