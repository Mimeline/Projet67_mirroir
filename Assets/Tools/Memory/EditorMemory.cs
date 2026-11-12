// Vide la mémoire inutilisée de l'éditeur (ressources plus référencées, ramasse-miettes C#) : à la main par
// Outils > Mémoire > Libérer maintenant, ou toutes les 10 minutes au repos si le nettoyage automatique est activé (désactivé par défaut).
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

namespace Tools.Memory
{
    [InitializeOnLoad]
    public static class EditorMemory
    {
        const double IntervalSeconds = 600;
        const string PrefKey = "Tools.Memory.Auto";
        const string AutoMenu = "Outils/Mémoire/Nettoyage automatique";

        static double next = EditorApplication.timeSinceStartup + IntervalSeconds;

        static EditorMemory() => EditorApplication.update += Tick;

        [MenuItem("Outils/Mémoire/Libérer maintenant")]
        public static void Release()
        {
            long before = Profiler.GetTotalReservedMemoryLong();
            EditorUtility.UnloadUnusedAssetsImmediate(true);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            long after = Profiler.GetTotalReservedMemoryLong();
            Debug.Log($"Mémoire de l'éditeur : {before >> 20} Mo → {after >> 20} Mo réservés.");
        }

        static void Tick()
        {
            if (EditorApplication.timeSinceStartup < next) return;
            next = EditorApplication.timeSinceStartup + IntervalSeconds;
            if (!EditorPrefs.GetBool(PrefKey, false)) return;
            if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isCompiling || EditorApplication.isUpdating) return;
            Release();
        }

        [MenuItem(AutoMenu)]
        static void Toggle() => EditorPrefs.SetBool(PrefKey, !EditorPrefs.GetBool(PrefKey, false));

        [MenuItem(AutoMenu, true)]
        static bool ToggleCheck()
        {
            Menu.SetChecked(AutoMenu, EditorPrefs.GetBool(PrefKey, false));
            return true;
        }
    }
}
