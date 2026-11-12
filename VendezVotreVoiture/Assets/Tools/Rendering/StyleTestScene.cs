// Scène d'essai du style « editorial Voodoo » : sol clair, formes simples aux couleurs vives, ciel en dégradé,
// brouillard teinté, une lumière chaude, caméra plongeante. Outils > Rendu > Créer la scène d'essai de style.
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Tools.Rendering
{
    public static class StyleTestScene
    {
        const string ScenePath = "Assets/Dev/Essais/Style/EssaiStyle.unity";

        [MenuItem("Outils/Rendu/Créer la scène d'essai de style")]
        public static void Build()
        {
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(ScenePath));
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            Atmosphere();
            CameraAndLight();
            Shapes();
            var volume = new GameObject("Global Volume").AddComponent<Volume>();
            volume.isGlobal = true;
            volume.sharedProfile = StyleAssets.Profile();
            EditorSceneManager.SaveScene(scene, ScenePath);
            AssetDatabase.SaveAssets();
            Debug.Log($"Scène d'essai de style créée : {ScenePath}");
        }

        static void Atmosphere()
        {
            RenderSettings.skybox = StyleAssets.Sky();
            RenderSettings.ambientMode = AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.75f, 0.85f, 1f);
            RenderSettings.ambientEquatorColor = new Color(1f, 0.85f, 0.78f);
            RenderSettings.ambientGroundColor = new Color(0.6f, 0.5f, 0.6f);
            RenderSettings.fog = true;
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogColor = new Color(1f, 0.86f, 0.76f);
            RenderSettings.fogStartDistance = 20f;
            RenderSettings.fogEndDistance = 60f;
        }

        static void CameraAndLight()
        {
            var cam = new GameObject("Main Camera") { tag = "MainCamera" }.AddComponent<Camera>();
            cam.transform.SetPositionAndRotation(new Vector3(0f, 9f, -15f), Quaternion.Euler(28f, 0f, 0f));
            cam.fieldOfView = 60f;
            cam.gameObject.AddComponent<UniversalAdditionalCameraData>().renderPostProcessing = true;
            var light = new GameObject("Soleil").AddComponent<Light>();
            light.type = LightType.Directional;
            light.color = new Color(1f, 0.95f, 0.86f);
            light.intensity = 1.25f;
            light.shadows = LightShadows.Soft;
            light.shadowStrength = 0.65f;
            light.transform.rotation = Quaternion.Euler(55f, -35f, 0f);
        }

        static void Shapes()
        {
            Place(PrimitiveType.Plane, Vector3.zero, new Vector3(10f, 1f, 10f), StyleAssets.Toon("Sol", new Color(1f, 0.86f, 0.74f), new Color(0.78f, 0.55f, 0.75f)));
            Place(PrimitiveType.Cube, new Vector3(-2f, 1f, 2f), Vector3.one * 2f, StyleAssets.Toon("Corail", new Color(1f, 0.45f, 0.4f), new Color(0.55f, 0.3f, 0.6f)));
            Place(PrimitiveType.Sphere, new Vector3(0.5f, 1f, 5f), Vector3.one * 2f, StyleAssets.Toon("Turquoise", new Color(0.2f, 0.82f, 0.78f), new Color(0.25f, 0.35f, 0.65f)));
            Place(PrimitiveType.Capsule, new Vector3(2.2f, 1.5f, 1f), Vector3.one * 1.5f, StyleAssets.Toon("Jaune", new Color(1f, 0.8f, 0.25f), new Color(0.75f, 0.4f, 0.45f)));
            Place(PrimitiveType.Cylinder, new Vector3(0f, 0.5f, -3f), new Vector3(2f, 0.5f, 2f), StyleAssets.Toon("Violet", new Color(0.62f, 0.45f, 1f), new Color(0.3f, 0.25f, 0.6f)));
        }

        static void Place(PrimitiveType type, Vector3 position, Vector3 scale, Material material)
        {
            var go = GameObject.CreatePrimitive(type);
            go.name = material.name;
            go.transform.position = position;
            go.transform.localScale = scale;
            go.GetComponent<Renderer>().sharedMaterial = material;
        }
    }
}
