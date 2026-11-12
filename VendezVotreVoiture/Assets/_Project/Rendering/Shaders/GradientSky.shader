// Ciel en dégradé à trois couleurs (haut, horizon, bas), sans texture : léger et net sur mobile.
Shader "Projet/GradientSky"
{
    Properties
    {
        _TopColor ("Haut", Color) = (0.35, 0.65, 1, 1)
        _HorizonColor ("Horizon", Color) = (1, 0.85, 0.7, 1)
        _BottomColor ("Bas", Color) = (0.95, 0.75, 0.65, 1)
        _Sharpness ("Netteté de l'horizon", Range(0.5, 8)) = 2
    }

    SubShader
    {
        Tags { "Queue" = "Background" "RenderType" = "Background" "PreviewType" = "Skybox" "RenderPipeline" = "UniversalPipeline" }
        Cull Off
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                half4 _TopColor;
                half4 _HorizonColor;
                half4 _BottomColor;
                half _Sharpness;
            CBUFFER_END

            struct Attributes { float4 positionOS : POSITION; };
            struct Varyings { float4 positionCS : SV_POSITION; float3 direction : TEXCOORD0; };

            Varyings Vert(Attributes v)
            {
                Varyings o;
                o.positionCS = TransformObjectToHClip(v.positionOS.xyz);
                o.direction = v.positionOS.xyz;
                return o;
            }

            half4 Frag(Varyings i) : SV_Target
            {
                half y = normalize(i.direction).y;
                half up = pow(saturate(y), 1 / _Sharpness);
                half down = pow(saturate(-y), 1 / _Sharpness);
                half3 color = lerp(_HorizonColor.rgb, _TopColor.rgb, up);
                color = lerp(color, _BottomColor.rgb, down);
                return half4(color, 1);
            }
            ENDHLSL
        }
    }
}
