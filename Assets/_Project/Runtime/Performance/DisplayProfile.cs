// Ce que l'écran permet : fréquence de rafraîchissement et échelle de rendu visant une densité cible.
using UnityEngine;

namespace Project.Performance
{
    public static class DisplayProfile
    {
        const int MaxFrameRate = 120;
        const int FallbackRefresh = 60;
        const float TargetDpi = 300f;
        const float MinScale = 0.5f;
        const int MaxRenderedLongSide = 2200;

        public static int RefreshRate()
        {
            var hz = Mathf.RoundToInt((float)Screen.currentResolution.refreshRateRatio.value);
            return Mathf.Clamp(hz > 0 ? hz : FallbackRefresh, 30, MaxFrameRate);
        }

        public static float RenderScale()
        {
            float byDpi = Screen.dpi > 0 ? TargetDpi / Screen.dpi : 1f;
            float byPixels = (float)MaxRenderedLongSide / Mathf.Max(Screen.width, Screen.height, 1);
            return Mathf.Clamp(Mathf.Min(byDpi, byPixels), MinScale, 1f);
        }
    }
}
