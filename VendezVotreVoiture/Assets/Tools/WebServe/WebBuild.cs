// Construit la version Web dans Builds/Web puis la sert sur le réseau local pour l'ouvrir depuis un téléphone.
using System.Linq;
using Tools.Network;
using UnityEditor;
using UnityEngine;

namespace Tools.WebServe
{
    public static class WebBuild
    {
        const string Output = "Builds/Web";

        [MenuItem("Outils/Web/Construire et servir")]
        static void BuildAndServe()
        {
            var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
            var report = BuildPipeline.BuildPlayer(scenes, Output, BuildTarget.WebGL, BuildOptions.None);
            if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                Debug.LogError("Build Web échouée : rien n'est servi.");
                return;
            }
            Serve();
        }

        [MenuItem("Outils/Web/Servir la dernière build")]
        static void Serve()
        {
            StaticServer.Start(Output);
            Debug.Log($"Build Web servie : http://{LocalNetwork.Address()}:{StaticServer.Port} (même Wi-Fi).");
        }

        [MenuItem("Outils/Web/Arrêter le serveur")]
        static void Stop()
        {
            StaticServer.Stop();
            Debug.Log("Serveur Web arrêté.");
        }
    }
}
