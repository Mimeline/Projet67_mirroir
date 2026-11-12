// Fabrique les scènes du squelette : Boot (permanente), décor 3D du lobby, interface du lobby.
using Project.Manager;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using Scene = UnityEngine.SceneManagement.Scene;

namespace Tools.Scenes
{
    public static class SceneFactories
    {
        public static void CreateBoot()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var boot = new GameObject("Boot");
            boot.AddComponent<SceneManager>();
            boot.AddComponent<GameManager>();
            boot.AddComponent<Boot>();
            Save(scene, SceneNames.Boot);
        }

        public static void CreateEnviro()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var cam = new GameObject("Main Camera") { tag = "MainCamera" };
            cam.AddComponent<Camera>();
            cam.AddComponent<AudioListener>();
            cam.transform.SetPositionAndRotation(new Vector3(0f, 2f, -6f), Quaternion.Euler(10f, 0f, 0f));
            var sun = new GameObject("Directional Light").AddComponent<Light>();
            sun.type = LightType.Directional;
            sun.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
            Save(scene, SceneNames.EnviroLobby);
        }

        public static void CreateUi()
        {
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var canvas = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
            Save(scene, SceneNames.UiLobby);
        }

        static void Save(Scene scene, string path)
        {
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
            EditorSceneManager.SaveScene(scene, path);
        }
    }
}
