# VendezVotreVoiture — projet Unity

Jeu mobile en portrait (iOS et Android), avec des essais en Web.

## Ouvrir le projet

- Unity **6000.0.85f2** (Unity 6 LTS). Ne pas ouvrir avec une autre version sans prévenir l'équipe : le projet serait converti.
- Modules à installer avec l'éditeur : Android Build Support (avec SDK, NDK et OpenJDK), iOS Build Support, Web Build Support.
- La plateforme active est **Android** (File > Build Profiles), pour que l'éditeur utilise le rendu mobile.
- À l'ouverture, la disposition de dev du projet s'applique (vue Simulator d'un iPhone à côté de la Scene). Pour la garder pour soi seulement : `Outils > Disposition de dev > Appliquer à l'ouverture`.

## Arborescence

| Dossier | Contenu |
|---|---|
| `Assets/_Project/` | uniquement le jeu : `Runtime/` (code), `Rendering/` (shaders) |
| `Assets/Tools/` | outils de l'éditeur, absents des builds : téléphone distant, menus `Outils`, appareils du simulateur |
| `Assets/Dev/` | tout ce qui sert aux tests : scènes de base (`Scenes/`), essais (`Essais/`) |
| `Assets/Settings/`, `Assets/TutorialInfo/` | venus du modèle Unity (URP mobile) |
| `Packages/`, `ProjectSettings/` | paquets et réglages du projet |

Chaque dossier de `_Project/`, `Tools/` et `Dev/` a son `README.md`.

`Library/`, `Temp/`, `Logs/`, `UserSettings/` et `Builds/` sont propres à chaque poste et ignorés par git.

## Outils du projet

- **Téléphone distant** : au Play, la console donne une adresse `http://<IP>:8765` ; ouverte sur un téléphone du même Wi-Fi, elle affiche la vue Game et renvoie le tactile au jeu. Interrupteur : `Outils > Téléphone distant au Play`.
- **Build Web servie en local** : `Outils > Web > Construire et servir` (adresse `http://<IP>:8766`).
- **Réglages** : `Outils > Configurer le projet mobile` (portrait, IL2CPP, ASTC…) et `Outils > Rendu > Appliquer les réglages mobiles` (MSAA, ombres, échelle de rendu). Rejouables.
- **Essai de style** : `Outils > Rendu > Créer la scène d'essai de style` → `Assets/Dev/Essais/Style/EssaiStyle.unity`.
- **Disposition de dev** : `Outils > Disposition de dev` (appliquer, enregistrer).

## Conventions

- Noms de code en anglais, textes et documentation en français.
- Un `README.md` par dossier de `_Project/`, `Tools/` et `Dev/` : qui fait quoi.
- Aucun commentaire dans le code, sauf un en-tête de trois lignes au plus.
- Une assembly par module (`*.asmdef`) ; le jeu dans `_Project/`, les outils dans `Tools/`, les tests dans `Dev/`.

## Pourquoi

À compléter.
