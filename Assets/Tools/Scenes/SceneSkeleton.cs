// Crée les scènes du squelette qui n'existent pas encore (sans toucher aux autres) et les inscrit dans le build, Root en premier.
// Menu : Outils > Scènes > Créer le squelette.
using System.IO;
using System.Linq;
using Project.Manager;
using UnityEditor;

namespace Tools.Scenes
{
    public static class SceneSkeleton
    {
        [MenuItem("Outils/Scènes/Créer le squelette")]
        public static void Build()
        {
            if (!File.Exists(SceneNames.Root)) SceneFactories.CreateBoot();
            if (!File.Exists(SceneNames.EnviroLobby)) SceneFactories.CreateEnviro();
            if (!File.Exists(SceneNames.UiLobby)) SceneFactories.CreateUi();
            AssetDatabase.SaveAssets();
            Register();
        }

        static void Register()
        {
            var ours = SceneNames.All.Select(path => new EditorBuildSettingsScene(path, true));
            var others = EditorBuildSettings.scenes.Where(s => !SceneNames.All.Contains(s.path));
            EditorBuildSettings.scenes = ours.Concat(others).ToArray();
        }
    }
}
