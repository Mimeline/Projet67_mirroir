// Réglages de performance choisis par le joueur, enregistrés sur l'appareil.
// 0 veut dire « Auto » : la fréquence suit l'écran, la résolution suit sa densité.
using UnityEngine;

namespace Project.Performance
{
    public static class PerformanceSettings
    {
        public const int Auto = 0;
        public static readonly int[] FrameRates = { Auto, 30, 60, 90, 120 };
        public static readonly int[] ResolutionPercents = { Auto, 50, 60, 70, 80, 90, 100 };

        const string FrameRateKey = "perf.frameRate";
        const string ResolutionKey = "perf.resolution";

        public static int FrameRate
        {
            get => PlayerPrefs.GetInt(FrameRateKey, Auto);
            set { PlayerPrefs.SetInt(FrameRateKey, value); PlayerPrefs.Save(); Adaptive.Apply(); }
        }

        public static int ResolutionPercent
        {
            get => PlayerPrefs.GetInt(ResolutionKey, Auto);
            set { PlayerPrefs.SetInt(ResolutionKey, value); PlayerPrefs.Save(); Adaptive.Apply(); }
        }
    }
}
