# PhoneRemote — téléphone distant au Play

Quand on lance Play dans l'éditeur, un serveur démarre sur le réseau local
(port 8765) ; la console donne l'adresse `http://<IP du PC>:8765`. Le
téléphone, sur le même Wi-Fi, ouvre cette page : il affiche la vue Game et
renvoie ses doigts au jeu comme un vrai écran tactile (Input System,
multitouch). Le serveur s'arrête avec Play. Interrupteur :
`Outils > Téléphone distant au Play`.

Code réservé à l'éditeur (assembly `PhoneRemote.Editor`), absent des builds.

- `PhoneRemoteSession.cs` : démarrage et arrêt avec Play, adresse dans la console, interrupteur.
- `Server/` : `RemoteServer` (TCP : page HTTP et WebSocket), `WebSocketFraming`, `ClientSocket` (un téléphone).
- `Capture/` : `FrameCapture`, capture de la vue Game, JPEG 720 px, 25 images/s. Sa propre assembly (`PhoneRemote.Capture`, compilée seulement dans l'éditeur) : un composant ne peut pas venir d'une assembly réservée à l'éditeur.
- `Input/` : `TouchInjector`, écran tactile virtuel de l'Input System.
- `Web/index.html` : la page du téléphone (plein écran, tactile).

Sur iPhone, « Ajouter à l'écran d'accueil » donne un vrai plein écran (Safari
n'a pas d'API plein écran sur iPhone).

Limites : le jeu tourne sur le PC (pas de mesure de performance mobile) ;
latence du Wi-Fi ; image en JPEG.

## Pourquoi

À compléter.
