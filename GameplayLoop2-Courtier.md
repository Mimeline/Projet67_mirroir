# Proposition 2 : Le courtier

Socle : [GameplayLoop.md](GameplayLoop.md). Brouillon du 2026-11-13.

Le joueur est un courtier. Il ne vend pas ce qu'il a : il cherche ce qu'on lui demande. Les acheteurs publient des demandes. Le joueur ouvre des conteneurs pour trouver la voiture, la rénove juste assez, et livre. Les voitures gardées forment un stock qui attend la bonne demande.

## Parti pris

- **Le temps** est réel. Les demandes ont une date d'expiration réelle. Le conteneur gratuit revient toutes les 8 heures.
- **Ce que le temps coûte** : chaque demande est valable un nombre d'heures ou de jours réels. Une voiture sans demande se vend 60 % de sa cote. Une voiture livrée sur demande se vend 150 % de sa cote. (example, ca varirais)
- **Le gacha a thème** : en plus des conteneurs par rareté, des conteneurs thématiques (une époque, une marque, un type) augmentent la chance de tirer ce qu'une demande réclame.
- **Les arnaques** sont de fausses demandes : budget hors de proportion, acheteur qui réclame une avance, modèle qui n'existe pas. Le joueur peut vérifier un acheteur, ce qui prend une heure réelle. Frank vérifie une demande gratuitement et tout de suite, une fois par semaine.
- **Le communautaire** : si demandes passent par le serveur, donc les demandes d'un joueur pourais être visibles pour les autres, et une voiture gardée par l'un peut répondre à la demande d'un autre.

## Chiffres provisoires

| Élément | Valeur |
|---|---|
| Argent de départ | 1 000 |
| Places au garage (stock) | 5, extensible  |
| Demandes actives | 3, extensible |
| Conteneur commun / rare / thématique | 300 / 1 000 / 2 000 |
| Valeur d'une voiture | 200 à 8 000 |
| Vente libre / sur demande | 60 % / 150 % de la cote |
| Validité d'une demande | 24 h au minimum, jusqu'à 7 jours |
| Vérifier un acheteur | 1 h réelle |
| Réparation | 1 à 12 h réelles |
| Conteneur gratuit | toutes les 8 h |

## Premier quart d'heure

| # | Écran | Le joueur voit | Il fait | Conséquence |
|---|---|---|---|---|
| 1 | Garage | 5 places vides, Frank, 1 000 d'argent | | |
| 2 | Tableau des demandes | 3 demandes : une citadine années 90 état ≥ 60 % budget 900 (12 j) · un utilitaire tout état budget 500 (8 j) · un coupé années 70 budget 6 000 avec avance de 500 réclamée (15 j) | Lit | |
| 3 | Conteneurs | Gratuit disponible. Commun 300. Thématique « années 90 » 2 000, trop cher | Ouvre le gratuit | Berline années 2000, état 70 %, cote 600 |
| 4 | Fiche voiture | Aucune demande ne correspond. Vente libre 360 · Garder | Garde en stock | Place 1 |
| 5 | Conteneurs | Commun 300 | Achète et ouvre | Utilitaire, état 45 %, cote 400. Argent 700 |
| 6 | Fiche voiture | Correspond à la demande 2 (tout état). Livrer 600 · Vente libre 240 · Garder | Livre | Argent 1 300. Demande 2 fermée, une nouvelle apparaît |
| 7 | Tableau des demandes | La demande 3 insiste sur l'avance | Vérifie l'acheteur | Vérification en cours, résultat dans 1 h. Frank relève l'avance réclamée |
| 8 | Conteneurs | Thématique « années 90 » 2 000 : toujours trop cher. Commun 300 | Achète et ouvre | Citadine années 90, état 50 %, cote 500. Argent 1 000 |
| 9 | Fiche voiture | Demande 1 exige état ≥ 60 %. Réparer jusqu'à 60 % : 2 h, 80 · Livrer ensuite 900 | Lance la réparation | Argent 920. Livraison possible dans 2 h, la demande reste valable 12 j |
| 10 | Garage | 2 voitures en stock dont 1 en travaux, vérification en cours, prochain gratuit dans 7 h 45 | Quitte | Reviendra livrer et lire le résultat de la vérification |

Durée : 8 à 12 minutes. Le joueur a vu les trois sorties d'une voiture : vente libre, stock, livraison (une faite, une en attente).


## Écrans propres à cette proposition

- Tableau des demandes, avec filtres par époque, type, état, budget.
- Correspondance demande ↔ voiture affichée sur la fiche.
- Conteneurs thématiques à côté des conteneurs par rareté.
- Historique de l'acheteur dans le détail d'une demande.

## Pour la production

- L'horloge est celle du serveur, comme dans la proposition 1.
- Génération des demandes à partir du catalogue de voitures : le catalogue structure tout le jeu (époque, type, marque, états).
- Les demandes sont servies par le serveur de l'équipe, ce qui permet le partage entre joueurs.

## Risques

- Le jeu dépend de la taille du catalogue de voitures : trop petit, les demandes se répètent.
- La correspondance demande ↔ voiture doit être lisible d'un coup d'œil.
