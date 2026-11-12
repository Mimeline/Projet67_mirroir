// Applique au démarrage, et à chaque changement de réglage, la fréquence d'images et l'échelle de rendu :
// celles du joueur s'il en a choisi, sinon celles de l'écran. En éditeur, l'asset URP n'est pas modifié.
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Project.Performance
{
    public static class Adaptive
    {
        public static int CurrentFrameRate { get; private set; }
        public static float CurrentRenderScale { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Apply()
        {
            int fps = PerformanceSettings.FrameRate;
            CurrentFrameRate = fps == PerformanceSettings.Auto ? DisplayProfile.RefreshRate() : fps;
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = CurrentFrameRate;
            int percent = PerformanceSettings.ResolutionPercent;
            CurrentRenderScale = percent == PerformanceSettings.Auto ? DisplayProfile.RenderScale() : percent / 100f;
            SetRenderScale(CurrentRenderScale);
            Debug.Log($"Performance : {CurrentFrameRate} images/s, échelle de rendu {CurrentRenderScale:0.00} (écran {Screen.dpi:0} dpi).");
        }

        static void SetRenderScale(float scale)
        {
#if !UNITY_EDITOR
            if (GraphicsSettings.currentRenderPipeline is UniversalRenderPipelineAsset urp) urp.renderScale = scale;
#endif
        }
    }
}
