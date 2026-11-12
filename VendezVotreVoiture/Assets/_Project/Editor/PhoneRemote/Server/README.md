# Server

- `RemoteServer.cs` : serveur TCP (port 8765) ; sert la page en HTTP, accepte les téléphones en WebSocket, diffuse les images.
- `WebSocketFraming.cs` : protocole WebSocket minimal (poignée de main, trames). Le Mono de Unity ne gère pas les WebSockets de `HttpListener`.
- `ClientSocket.cs` : un téléphone connecté ; une seule image en attente d'envoi, réception des messages tactiles.
