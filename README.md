Test de compétence du LabXR de Liège

Réalisé par:
- Titouan DELEPORTE

Aucune Intelligence Artificielle n'a été utilisée durant ce travail

Structure du projet situé dans le dossier LabXR_TechnicalTest2026:

LabXR_TechnicalTest2026

├── Materials                 // le dossier contenant les materiaux utilisés sur les objets du jeu

├── Prefabs              // le dossier servant à conserver les préfabs du jeu

├── Scenes                 // les 2 scenes unity (MenuPrincipal et Jeu)

├── Scripts         // le dossier contenant les scripts du jeu

├── Settings et TutorialInfo              // les dossiers présents à la création du projet

└── TextMesh Pro (Default Asset)            // Le dossier contenant les textures utilisées pour les textes

================================================================================

Prérequis d'utilisation:
-----------------------
	1. Unity 6000.3.8f1
	   (https://unity.com/fr/releases/editor/whats-new/6000.3.8f1)

Choix d'implémentation

Pour l'implémentation de la grille : 

├── Matrice d'une classe de case : La matrice instancie toutes les cases au début de la partie

└── Chaque case possède un champ qui dit si elle est occupée ou non


Pour l'algorithme de detection de chemin :

Algorithme A*

├── Renvoie un tableau de Vector2 contenant les indices des cases à visiter dans l'ordre             

├── Sécurité de vérification : Renvoie une liste vide si on ne peut pas trouver de chemin

└── L'algorithme est rappelé à chaque fois qu'on pose un mur

Reset

├── Parcours notre grille et en retire tous les murs : Si la case est marquée comme murée, elle devient Libre et sa couleur est ramenée à la normale

└── Réinitialiser les murs ne rappelle PAS A* ce qui peut donner au pion un trajet compliqué alors qu'un chemin plus court existe 			// Ceci est un choix car implémenter cette fonctionnalité alourdirait grandement le code


Pour l'instanciation des objets :

├── Un objet existe sur la scène de jeu contenant les préfabs des autres objets

└── La finalité serait de procéder par Addressables


Pour le transfert des données

├── Classe d'instance statique référencée dans les scripts d'intialisation

└── L'objet associé est transformé en "DoNotDestroy" pour qu'il reste entre les scènes