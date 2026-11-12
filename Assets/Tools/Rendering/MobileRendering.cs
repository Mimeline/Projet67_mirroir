// Réglages techniques du rendu mobile (Mobile_RPAsset, Mobile_Renderer) : MSAA x4, résolution 0,85,
// une lumière principale avec ombres douces courtes, pas de lumières additionnelles, forward simple.
// Rejouable : Outils > Rendu > Appliquer les réglages mobiles, ou -executeMethod Tools.Rendering.MobileRendering.Apply.
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Tools.Rendering
{
    public static class MobileRendering
    {
        const string PipelinePath = "Assets/Settings/Mobile_RPAsset.asset";
        const string RendererPath = "Assets/Settings/Mobile_Renderer.asset";

        static readonly Dictionary<string, float> Pipeline = new Dictionary<string, float>
        {
            { "m_MSAA", 4 }, { "m_RenderScale", 0.85f }, { "m_UpscalingFilter", 0 }, { "m_SupportsHDR", 0 },
            { "m_MainLightShadowsSupported", 1 }, { "m_MainLightShadowmapResolution", 1024 },
            { "m_ShadowDistance", 30 }, { "m_ShadowCascadeCount", 1 }, { "m_SoftShadowsSupported", 1 },
            { "m_SoftShadowQuality", 1 }, { "m_AdditionalLightsRenderingMode", 0 },
            { "m_AdditionalLightShadowsSupported", 0 }, { "m_RequireDepthTexture", 0 },
            { "m_RequireOpaqueTexture", 0 }, { "m_UseSRPBatcher", 1 },
        };

        static readonly Dictionary<string, float> Renderer = new Dictionary<string, float>
        {
            { "m_RenderingMode", 0 }, { "m_DepthPrimingMode", 0 },
        };

        [MenuItem("Outils/Rendu/Appliquer les réglages mobiles")]
        public static void Apply()
        {
            Set(PipelinePath, Pipeline);
            Set(RendererPath, Renderer);
            AssetDatabase.SaveAssets();
            Debug.Log("Rendu mobile : MSAA x4, échelle 0,85, sans HDR, ombres douces à 30 m, forward, sans lumières additionnelles.");
        }

        static void Set(string path, Dictionary<string, float> values)
        {
            var asset = AssetDatabase.LoadMainAssetAtPath(path);
            if (asset == null) { Debug.LogError($"Introuvable : {path}"); return; }
            var so = new SerializedObject(asset);
            foreach (var pair in values)
            {
                var p = so.FindProperty(pair.Key);
                if (p == null) { Debug.LogWarning($"{path} : réglage {pair.Key} absent dans cette version d'URP."); continue; }
                if (p.propertyType == SerializedPropertyType.Float) p.floatValue = pair.Value;
                else if (p.propertyType == SerializedPropertyType.Boolean) p.boolValue = pair.Value != 0;
                else p.intValue = (int)pair.Value;
            }
            so.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
