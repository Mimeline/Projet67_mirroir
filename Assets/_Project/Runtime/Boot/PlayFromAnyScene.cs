// En éditeur : au Play depuis une autre scène que Boot, ajoute Boot par-dessus (chargement immédiat)
// pour que les gestionnaires existent avant le premier Start. Sans effet dans un build.
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Project.Manager
{
    public static class PlayFromAnyScene
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void EnsureBoot()
        {
#if UNITY_EDITOR
            if (UnitySceneManager.GetSceneByPath(SceneNames.Boot).isLoaded) return;
            UnitySceneManager.LoadScene(SceneNames.Boot, LoadSceneMode.Additive);
#endif
        }
    }
}
