// Ombrage stylisé mobile (URP) : couleur pleine, passage doux lumière/ombre vers une couleur d'ombre teintée,
// ombres portées de la lumière principale, liseré de contre-jour, brouillard. Une seule passe éclairée.
Shader "Projet/ToonLit"
{
    Properties
    {
        _BaseColor ("Couleur", Color) = (1, 0.55, 0.3, 1)
        _ShadeColor ("Couleur d'ombre", Color) = (0.45, 0.35, 0.65, 1)
        _RampCenter ("Seuil lumière/ombre", Range(-1, 1)) = 0.1
        _RampSmooth ("Douceur du seuil", Range(0.001, 1)) = 0.15
        _RimColor ("Liseré", Color) = (1, 1, 1, 0.35)
        _RimPower ("Finesse du liseré", Range(1, 8)) = 4
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" "Queue" = "Geometry" }

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile_fragment _ _SHADOWS_SOFT _SHADOWS_SOFT_LOW _SHADOWS_SOFT_MEDIUM _SHADOWS_SOFT_HIGH
            #pragma multi_compile_fog
            #pragma multi_compile_instancing
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                half4 _ShadeColor;
                half _RampCenter;
                half _RampSmooth;
                half4 _RimColor;
                half _RimPower;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
                half3 normalWS : TEXCOORD1;
                half fog : TEXCOORD2;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            Varyings Vert(Attributes v)
            {
                Varyings o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                VertexPositionInputs p = GetVertexPositionInputs(v.positionOS.xyz);
                o.positionCS = p.positionCS;
                o.positionWS = p.positionWS;
                o.normalWS = TransformObjectToWorldNormal(v.normalOS);
                o.fog = ComputeFogFactor(p.positionCS.z);
                return o;
            }

            half4 Frag(Varyings i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                half3 n = normalize(i.normalWS);
                Light light = GetMainLight(TransformWorldToShadowCoord(i.positionWS));
                half ndl = dot(n, light.direction);
                half lit = smoothstep(_RampCenter - _RampSmooth, _RampCenter + _RampSmooth, ndl) * light.shadowAttenuation;
                half3 color = lerp(_ShadeColor.rgb * _BaseColor.rgb, _BaseColor.rgb * light.color, lit);
                color += SampleSH(n) * _BaseColor.rgb * 0.25;
                half3 view = normalize(GetWorldSpaceViewDir(i.positionWS));
                half rim = pow(1 - saturate(dot(n, view)), _RimPower) * _RimColor.a;
                color += _RimColor.rgb * rim;
                color = MixFog(color, i.fog);
                return half4(color, 1);
            }
            ENDHLSL
        }

        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
        UsePass "Universal Render Pipeline/Lit/DepthOnly"
    }
}
