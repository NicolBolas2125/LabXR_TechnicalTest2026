using System.Collections.Generic;
using UnityEngine;

public class AEtoile : MonoBehaviour
{
    // Destination visée par A*
    private Vector2 _cible;

    // Permet d'accéder à la variable hors de la classe
    public Vector2 State
    {
        get { return _cible; }
        set { _cible = value; }
    }


    // Renvoie la distance parcourue pour arriver à la case
    private int G(int gCaseActuelle)
    {
        // On ne se déplace que d'une case à la fois et tous les trajets sont de taille 1
        return gCaseActuelle + 1;
    }

    // Renvoie l'heuristique de A* en utilisant la distance de Manhattan
    private int H(int xActuel, int zActuel)
    {
        // On n'opère sur des cases avec des indices entiers, on peut donc faire un cast sans problème de valeurs arrondies
        return (int) (Mathf.Abs(_cible.x - xActuel) + Mathf.Abs(_cible.y - zActuel));
    }

    private int f(int xActuel, int zActuel, int gCaseActuelle)
    {
        return G(gCaseActuelle) + H(xActuel, zActuel);
    }

    // Reconstitue le chemin à partir de la liste des sommets traites et de G le nombre de cases de ce chemin
    private Vector2[] chemin(List<(Vector2, Vector2)> traites, int g)
    {
        // On crée le trajeet de taille g (A* a trouvé l'arrvée)
        Vector2[] trajet = new Vector2[g];
        
        // Initialise la position où on introduit nos éléments
        int i = g - 1;
        
        // Définie l'arrivée comme la cible donnée au départ
        trajet[i] = _cible;

        // Élément qu'on va chercher dans notre liste des sommets traités
        Vector2 cherche = _cible;

        // Recherche
        while (i > 0)
        {
            // On cherche le couple qui pointe vers le sommet qu'on cherche
            (Vector2, Vector2) suivant = traites.Find(element => element.Item1 == cherche);
            
            // ON récupère le sommet qui précède et on repart dans la recherche de l'élément suivant
            trajet[i] = suivant.Item2;
            i = i - 1;
            cherche = suivant.Item2;
        }

        // Renvoi du chemin trouvé
        return trajet;
    }


    public Vector2[] Aetoile(int xDepart, int zDepart)
    {
        // Initialise G à 0 puisque notre fonction G prend en paramètre le G de la case actuelle
        int g = 0;
        // Initialise les coordonnées
        int xActuel = xDepart;
        int zActuel = zDepart;

        // Récupère l'accès à la grille
        GenerateurDeGrille grille = GenerateurDeGrille.instance;

        // Génére une grille des sommets traités
        bool[,] traites = new bool[grille._tailleGrille, grille._tailleGrille];

        // Donne les listes des sommets avec leur origine quand ils sont parcourus
        List<(Vector2, Vector2, int)> aTraiter = new List<(Vector2, Vector2, int)>();
        List<(Vector2, Vector2)> estTraite = new List<(Vector2, Vector2)>();
        

        // Vérifie qu'on est pas arrivé à destination
        bool fini = (xActuel == _cible.x) && (zActuel == _cible.y);

        // Execution de l'algorithme A*
        while (!fini)
        {
            // Le sommet actuel est marqué comme traité
            traites[xActuel, zActuel] = true;

            // Extraction des sommets alentours utilisables
            // Droite
            if  (xActuel + 1 < grille._tailleGrille)
            {
                if (traites[xActuel + 1, zActuel] == false)
                {
                    var celluleDeDroite = grille._grille[xActuel + 1, zActuel];
                    if (celluleDeDroite.Etat != CelluleDeGrille.EtatDeCellule.Muree)
                    {
                        aTraiter.Add((new Vector2(xActuel + 1, zActuel), new Vector2(xActuel, zActuel), g));
                    }
                }
            }
            // Gauche
            if  (xActuel - 1 >= 0)
            {
                if (traites[xActuel - 1, zActuel] == false)
                {
                    var celluleDeGauche = grille._grille[xActuel - 1, zActuel];
                    if (celluleDeGauche.Etat != CelluleDeGrille.EtatDeCellule.Muree)
                    {
                        aTraiter.Add((new Vector2(xActuel - 1, zActuel), new Vector2(xActuel, zActuel), g));
                    }
                }
            }
            // Haut
            if  (zActuel + 1 < grille._tailleGrille)
            {
                if (traites[xActuel, zActuel + 1] == false)
                {
                    var CelluleDuHaut = grille._grille[xActuel, zActuel + 1];
                    if (CelluleDuHaut.Etat != CelluleDeGrille.EtatDeCellule.Muree)
                    {
                        aTraiter.Add((new Vector2(xActuel, zActuel + 1), new Vector2(xActuel, zActuel), g));
                    }
                }
            }
            // Bas
            if  (zActuel - 1 >= 0)
            {
                if (traites[xActuel, zActuel - 1] == false)
                {
                    var CelluleDuBas = grille._grille[xActuel, zActuel - 1];
                    if (CelluleDuBas.Etat != CelluleDeGrille.EtatDeCellule.Muree)
                    {
                        aTraiter.Add((new Vector2(xActuel, zActuel - 1), new Vector2(xActuel, zActuel), g));
                    }
                }
            }


            // Maintenant on décide quel sommet sera le suivant
            (Vector2, Vector2, int) suivant = aTraiter[0];
            int fSuivant = f((int) suivant.Item1.x, (int) suivant.Item1.y, suivant.Item3);
            foreach (var element in aTraiter)
            {
                int fATester = f((int) element.Item1.x, (int) element.Item1.y, element.Item3);
                if (fATester < fSuivant)
                {
                    suivant = element;
                    fSuivant = fATester;
                }
            }

            // On a notre élément suivant, on le retire de la liste "à traiter" et on l'ajoute à la liste "est traité"
            aTraiter.Remove(suivant);
            estTraite.Add((suivant.Item1, suivant.Item2));
            
            // On définie nos variables pour le tour de boucle suivant
            xActuel = (int) suivant.Item1.x;
            zActuel = (int) suivant.Item1.y;
            g = G(suivant.Item3);
            fini = (xActuel == _cible.x) && (zActuel == _cible.y);
        }

        // On reconstruit le chemin à partir de notre tableau "est traité"
        return chemin(estTraite, g);
    }
}
