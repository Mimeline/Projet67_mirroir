// Chemins des scènes du jeu : l'unique définition, utilisée par SceneManager, Boot et l'outil de création.
namespace Project.Manager
{
    public static class SceneNames
    {
        public const string Root = "Assets/_Project/Scenes/Root.unity";
        public const string EnviroLobby = "Assets/_Project/Scenes/Enviro/Env-Lobby.unity";
        public const string UiLobby = "Assets/_Project/Scenes/UI/UI-Lobby.unity";

        public static readonly string[] All = { Root, EnviroLobby, UiLobby };
    }
}
