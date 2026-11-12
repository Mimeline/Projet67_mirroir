# Memory

- `EditorMemory.cs` : vide la mémoire inutilisée de l'éditeur (ressources plus référencées, ramasse-miettes C#) à la main, ou toutes les 10 minutes au repos (jamais pendant le Play ni une compilation) si le nettoyage automatique est activé : **désactivé par défaut**, à activer sur les postes à peu de mémoire. Menus `Outils > Mémoire > Libérer maintenant` et `Nettoyage automatique` (interrupteur, par poste).

Limite : la mémoire libérée par Unity n'est pas toujours rendue au système tout de suite ; elle redevient disponible pour l'éditeur, ce qui l'empêche de grossir.
