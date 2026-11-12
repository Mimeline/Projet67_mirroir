# WebServe

- `WebBuild.cs` : menus `Outils > Web` : construire et servir, servir la dernière build, arrêter.
- `StaticServer.cs` : serveur de fichiers sur le réseau local (port 8766), types MIME de Unity Web.

La build va dans `Builds/Web` (ignoré par git). La décompression de secours évite tout réglage d'en-têtes côté serveur.
