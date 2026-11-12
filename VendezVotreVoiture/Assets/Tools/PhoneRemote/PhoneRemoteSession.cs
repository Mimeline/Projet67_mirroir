// Démarre le téléphone distant à l'entrée en Play et l'arrête à la sortie ; interrupteur dans le menu Outils.
using PhoneRemote.Capture;
using PhoneRemote.Input;
using PhoneRemote.Server;
using Tools.Network;
using UnityEditor;
using UnityEngine;

namespace PhoneRemote
{
    [InitializeOnLoad]
    public static class PhoneRemoteSession
    {
        const string PrefKey = "PhoneRemote.Enabled";
        const string MenuPath = "Outils/Téléphone distant au Play";
        const string PagePath = "Assets/Tools/PhoneRemote/Web/index.html";

        static GameObject capture;

        static PhoneRemoteSession() => EditorApplication.playModeStateChanged += OnPlayMode;

        [MenuItem(MenuPath)]
        static void Toggle() => EditorPrefs.SetBool(PrefKey, !EditorPrefs.GetBool(PrefKey, true));

        [MenuItem(MenuPath, true)]
        static bool ToggleCheck() { Menu.SetChecked(MenuPath, EditorPrefs.GetBool(PrefKey, true)); return true; }

        static void OnPlayMode(PlayModeStateChange change)
        {
            if (change == PlayModeStateChange.EnteredPlayMode && EditorPrefs.GetBool(PrefKey, true)) Begin();
            if (change != PlayModeStateChange.ExitingPlayMode || capture == null) return;
            EditorApplication.update -= TouchInjector.Drain;
            TouchInjector.Detach();
            RemoteServer.Stop();
            Object.DestroyImmediate(capture);
            capture = null;
        }

        static void Begin()
        {
            RemoteServer.Start(PagePath, TouchInjector.Receive);
            TouchInjector.Attach();
            EditorApplication.update += TouchInjector.Drain;
            capture = new GameObject("PhoneRemote") { hideFlags = HideFlags.HideAndDontSave };
            Object.DontDestroyOnLoad(capture);
            capture.AddComponent<FrameCapture>().OnFrame = RemoteServer.Broadcast;
            Debug.Log($"Téléphone distant : ouvrir http://{LocalNetwork.Address()}:{RemoteServer.Port} sur le téléphone (même Wi-Fi).");
        }
    }
}
