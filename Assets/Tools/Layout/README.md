# Layout

- `Dev.wlt` : disposition de dev mobile. Hiérarchie à gauche, Scene au centre, vue Game seule dans une colonne étroite à droite (à passer en mode Simulator, iPhone en portrait), Inspector tout à droite, Project et Console en bas. Écrite à partir de la disposition par défaut de Unity 6000.0.
- `DevLayout.cs` : menus `Outils > Disposition de dev` : appliquer `Dev.wlt`, enregistrer la disposition actuelle dans `Dev.wlt`.
- `DevLayoutStartup.cs` : disposition par défaut du projet, appliquée une fois à chaque ouverture de l'éditeur ; désactivable par personne (`Outils > Disposition de dev > Appliquer à l'ouverture`).
