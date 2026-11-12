// Disposition de dev partagée (vue Simulator d'un iPhone sur le côté), versionnée dans le dépôt.
// Outils > Disposition de dev > Appliquer / Enregistrer la disposition actuelle.
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Tools.Layout
{
    public static class DevLayout
    {
        public const string Path = "Assets/Tools/Layout/Dev.wlt";

        [MenuItem("Outils/Disposition de dev/Appliquer")]
        static void Apply()
        {
            if (!System.IO.File.Exists(Path)) { Debug.LogWarning($"Aucune disposition enregistrée dans {Path}."); return; }
            EditorUtility.LoadWindowLayout(Path);
        }

        [MenuItem("Outils/Disposition de dev/Enregistrer la disposition actuelle")]
        static void Save()
        {
            var type = typeof(Editor).Assembly.GetType("UnityEditor.WindowLayout");
            var save = type?.GetMethod("SaveWindowLayout", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                null, new[] { typeof(string) }, null);
            if (save == null) { Debug.LogError("Cette version de Unity n'expose plus l'enregistrement de disposition."); return; }
            save.Invoke(null, new object[] { Path });
            AssetDatabase.Refresh();
            Debug.Log($"Disposition de dev enregistrée dans {Path} : à committer pour la partager.");
        }
    }
}
