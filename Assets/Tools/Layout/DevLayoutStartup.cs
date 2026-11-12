// Disposition par défaut du projet : applique Dev.wlt une fois à chaque ouverture de l'éditeur.
// Désactivable par personne : Outils > Disposition de dev > Appliquer à l'ouverture.
using UnityEditor;

namespace Tools.Layout
{
    [InitializeOnLoad]
    public static class DevLayoutStartup
    {
        const string SessionKey = "Project.DevLayout.Applied";
        const string PrefKey = "Project.DevLayout.OnStartup";
        const string MenuPath = "Outils/Disposition de dev/Appliquer à l'ouverture";

        static DevLayoutStartup()
        {
            if (SessionState.GetBool(SessionKey, false) || !EditorPrefs.GetBool(PrefKey, true)) return;
            SessionState.SetBool(SessionKey, true);
            EditorApplication.delayCall += () => EditorUtility.LoadWindowLayout(DevLayout.Path);
        }

        [MenuItem(MenuPath)]
        static void Toggle() => EditorPrefs.SetBool(PrefKey, !EditorPrefs.GetBool(PrefKey, true));

        [MenuItem(MenuPath, true)]
        static bool ToggleCheck()
        {
            Menu.SetChecked(MenuPath, EditorPrefs.GetBool(PrefKey, true));
            return true;
        }
    }
}
