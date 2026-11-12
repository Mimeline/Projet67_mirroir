// Ressources de l'essai de style : matériaux ToonLit, ciel en dégradé, profil de post-traitement léger.
// Créées ou mises à jour dans Assets/Dev/Essais/Style.
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Tools.Rendering
{
    public static class StyleAssets
    {
        public const string Folder = "Assets/Dev/Essais/Style";

        public static Material Toon(string name, Color color, Color shade)
        {
            var m = Load<Material>(name + ".mat") ?? Create(new Material(Shader.Find("Projet/ToonLit")), name + ".mat");
            m.SetColor("_BaseColor", color);
            m.SetColor("_ShadeColor", shade);
            m.enableInstancing = true;
            EditorUtility.SetDirty(m);
            return m;
        }

        public static Material Sky()
        {
            var m = Load<Material>("Ciel.mat") ?? Create(new Material(Shader.Find("Projet/GradientSky")), "Ciel.mat");
            m.SetColor("_TopColor", new Color(0.33f, 0.62f, 1f));
            m.SetColor("_HorizonColor", new Color(1f, 0.86f, 0.74f));
            m.SetColor("_BottomColor", new Color(0.98f, 0.78f, 0.7f));
            EditorUtility.SetDirty(m);
            return m;
        }

        public static VolumeProfile Profile()
        {
            var p = Load<VolumeProfile>("PostTraitement.asset") ?? Create(ScriptableObject.CreateInstance<VolumeProfile>(), "PostTraitement.asset");
            var tone = p.TryGet(out Tonemapping t) ? t : p.Add<Tonemapping>(true);
            tone.mode.Override(TonemappingMode.Neutral);
            var grade = p.TryGet(out ColorAdjustments c) ? c : p.Add<ColorAdjustments>(true);
            grade.saturation.Override(18f);
            grade.contrast.Override(8f);
            var bloom = p.TryGet(out Bloom b) ? b : p.Add<Bloom>(true);
            bloom.intensity.Override(0.35f);
            bloom.threshold.Override(0.9f);
            foreach (var component in p.components) if (!AssetDatabase.Contains(component)) AssetDatabase.AddObjectToAsset(component, p);
            EditorUtility.SetDirty(p);
            return p;
        }

        static T Load<T>(string file) where T : Object => AssetDatabase.LoadAssetAtPath<T>($"{Folder}/{file}");

        static T Create<T>(T asset, string file) where T : Object
        {
            System.IO.Directory.CreateDirectory(Folder);
            AssetDatabase.CreateAsset(asset, $"{Folder}/{file}");
            return asset;
        }
    }
}
