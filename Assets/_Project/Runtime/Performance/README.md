# Performance

Fréquence d'images et résolution de rendu adaptées à l'écran, sauf si le joueur a choisi les siennes.

- `Adaptive.cs` : applique les deux réglages au démarrage et à chaque changement ; expose les valeurs en cours (`CurrentFrameRate`, `CurrentRenderScale`). En éditeur, l'asset URP n'est pas modifié.
- `DisplayProfile.cs` : ce que l'écran permet : sa fréquence de rafraîchissement (60, 90, 120 Hz, plafonnée à 120) et l'échelle de rendu qui vise 300 dpi (bornée entre 0,5 et 1, et à 2200 pixels sur le grand côté).
- `PerformanceSettings.cs` : choix du joueur enregistrés sur l'appareil : `FrameRate` (0 = Auto, 30, 60, 90, 120) et `ResolutionPercent` (0 = Auto, 50 à 100). Le futur menu d'options écrit ici.
