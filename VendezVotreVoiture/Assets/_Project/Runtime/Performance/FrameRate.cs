// Fixe le jeu à 60 images par seconde au démarrage (sur mobile, Unity vise 30 par défaut).
using UnityEngine;

namespace Project.Performance
{
    public static class FrameRate
    {
        const int Target = 60;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Apply()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = Target;
        }
    }
}
