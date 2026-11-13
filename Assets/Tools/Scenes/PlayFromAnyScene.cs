// Au Play depuis une autre scène que Root, ajoute Root par-dessus (chargement immédiat)
// pour que les gestionnaires existent avant le premier Start. Éditeur seulement, absent des builds.
using Project.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Tools.Scenes
{
    public static class PlayFromAnyScene
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void EnsureRoot()
        {
            if (UnitySceneManager.GetSceneByPath(SceneNames.Root).isLoaded) return;
            UnitySceneManager.LoadScene(SceneNames.Root, LoadSceneMode.Additive);
        }
    }
}
