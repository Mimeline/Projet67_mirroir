// Réglages du projet pour un jeu mobile en portrait : iOS, Android, Web.
// Rejouable : Outils > Configurer le projet mobile, ou -executeMethod Project.Tooling.Setup.MobileSetup.Apply.
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace Project.Tooling.Setup
{
    public static class MobileSetup
    {
        [MenuItem("Outils/Configurer le projet mobile")]
        public static void Apply()
        {
            Orientation();
            Mobile();
            Web();
            AssetDatabase.SaveAssets();
            Debug.Log("Projet configuré : portrait, IL2CPP ARM64, ASTC, Web avec décompression de secours.");
        }

        static void Orientation()
        {
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
            PlayerSettings.allowedAutorotateToPortrait = true;
            PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
            PlayerSettings.allowedAutorotateToLandscapeLeft = false;
            PlayerSettings.allowedAutorotateToLandscapeRight = false;
        }

        static void Mobile()
        {
            PlayerSettings.SetScriptingBackend(NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetScriptingBackend(NamedBuildTarget.iOS, ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            EditorUserBuildSettings.androidBuildSubtarget = MobileTextureSubtarget.ASTC;
        }

        static void Web()
        {
            PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Gzip;
            PlayerSettings.WebGL.decompressionFallback = true;
        }
    }
}
