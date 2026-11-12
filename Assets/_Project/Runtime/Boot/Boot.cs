// Objet Boot, dans la scène permanente : indestructible, il porte les gestionnaires.
// Il charge le lobby (décor 3D, puis interface), sauf si une autre scène est déjà ouverte (test en éditeur).
using UnityEngine;

namespace Project.Manager
{
    [RequireComponent(typeof(SceneManager))]
    public class Boot : MonoBehaviour
    {
        void Awake() => DontDestroyOnLoad(gameObject);

        async void Start()
        {
            if (UnityEngine.SceneManagement.SceneManager.sceneCount > 1) return;
            var scenes = GetComponent<SceneManager>();
            await scenes.Load(SceneNames.EnviroLobby);
            scenes.Activate(SceneNames.EnviroLobby);
            await scenes.Load(SceneNames.UiLobby);
        }
    }
}
