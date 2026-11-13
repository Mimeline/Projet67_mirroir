# Proposition 1 : Le revendeur

Socle : [GameplayLoop.md](GameplayLoop.md). Brouillon du 2026-11-13.

Le joueur est un revendeur à la petite semaine. Il fait des coups rapides. Le jeu s'ouvre plusieurs fois par jour, quelques minutes à chaque fois.

## Parti pris

- **Le temps** est réel. Les réparations durent des heures réelles. Les offres ont une date d'expiration réelle. Le conteneur gratuit revient toutes les 8 heures.
- **Ce que le temps coûte** : une offre expirée est perdue. Une voiture au garage bloque une place. La cote d'un modèle bouge chaque jour.
- **Les arnaques** sont dans l'offre elle-même : prix très au-dessus de la cote, paiement « à la livraison », acheteur sans historique. Le joueur compare l'offre et la fiche de la voiture. S'il accepte une arnaque, il perd la voiture. Frank ne prévient pas en amont, mais il commente après coup, et le joueur apprend avec sum.

## Chiffres provisoires

| Élément | Valeur |
|---|---|
| Argent de départ | 1 000 |
| Places au garage | 3, extensible  |
| Conteneur commun / rare / épique | 400 / 1 500 / 5 000 |
| Valeur d'une voiture | 200 à 8 000 |
| Réparation | 1 à 12 h réelles, coûte 20 à 40 % de la cote, rapporte 30 à 80 % |
| Conteneur gratuit | toutes les 8 h |
| Validité d'une offre | 24 h au minimum, jusqu'à 72 h |

## Premier quart d'heure

| # | Écran | Le joueur voit | Il fait | Conséquence |
|---|---|---|---|---|
| 1 | Garage | Trois places vides, Frank, 1 000 d'argent | Suit Frank vers les conteneurs | Tutoriel par l'action |
| 2 | Conteneurs | Un conteneur gratuit disponible, un commun à 400 | Ouvre le gratuit | |
| 3 | Ouverture | Révélation : voiture commune, état 40 %, cote 350 | | La voiture entre en place 1 |
| 4 | Fiche voiture | Vendre 350 · Réparer (6 h, 120, cote après 600) · Garder | Lance la réparation | Argent 880, place 1 en travaux jusqu'à H+6 |
| 5 | Garage | Place 1 : jauge 6 h. Pastille sur la messagerie | Ouvre la messagerie | |
| 6 | Messagerie | Un acheteur cherche un modèle précis, budget 900, valable 24 h | Lit, ne peut pas répondre : il n'a pas la voiture | Première demande en attente |
| 7 | Conteneurs | Commun à 400 | Achète et ouvre | Argent 480. Voiture 2, état 70 %, cote 500, place 2 |
| 8 | Fiche voiture | Vendre 500 · Réparer (2 h, 100, cote 650) · Garder | Vend tout de suite | Argent 980, place 2 libre |
| 9 | Garage | Place 1 en travaux, 2 et 3 libres, prochain gratuit dans 7 h 50 | Quitte le jeu | Il n'a plus d'action utile |

Durée : 5 à 8 minutes. Fin de session naturelle : tout ce qui pouvait être fait l'a été.


## Écrans propres à cette proposition

- Jauges de temps réel sur les places du garage et sur les offres.
- Historique de l'acheteur dans le détail d'une offre.
- Courbe de cote par modèle sur la fiche voiture.

## Technique

- L'horloge est celle du serveur  : le joueur ne peut pas tricher en changeant l'heure du téléphone.
- Les notifications ?? 
- Génération des offres et des arnaques à partir de la cote et de l'historique de l'acheteur. (je vois ca un peut complex, c'est un algo)



