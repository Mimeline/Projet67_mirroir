// Chemins des scènes du jeu : l'unique définition, utilisée par SceneManager, Boot et l'outil de création.
namespace Project.Manager
{
    public static class SceneNames
    {
        public const string Boot = "Assets/_Project/Scenes/Boot.unity";
        public const string EnviroLobby = "Assets/_Project/Scenes/Enviro/Lobby.unity";
        public const string UiLobby = "Assets/_Project/Scenes/UI/Lobby.unity";

        public static readonly string[] All = { Boot, EnviroLobby, UiLobby };
    }
}
