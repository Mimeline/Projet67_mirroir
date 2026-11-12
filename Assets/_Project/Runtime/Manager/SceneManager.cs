// Composant de l'objet Boot : charge et décharge les scènes en additif, par chemin (SceneNames).
// La scène active fournit l'éclairage ambiant.
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Project.Manager
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        void Awake() => Instance = this;

        public bool IsLoaded(string path) => UnitySceneManager.GetSceneByPath(path).isLoaded;

        public async Awaitable Load(string path)
        {
            if (IsLoaded(path)) return;
            await UnitySceneManager.LoadSceneAsync(path, LoadSceneMode.Additive);
        }

        public async Awaitable Unload(string path)
        {
            if (!IsLoaded(path)) return;
            await UnitySceneManager.UnloadSceneAsync(path);
        }

        public void Activate(string path) => UnitySceneManager.SetActiveScene(UnitySceneManager.GetSceneByPath(path));
    }
}
