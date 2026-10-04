using UnityEngine;

public class CelluleDeGrille : MonoBehaviour
{
    // Référence au 3 états possible d'une case : 
    // - Libre (sans rien dessus), 
    // - Occupée (le joueur est dessus ou s'y déplace)
    // - Murée (cette case est bloquée)
    public enum EtatDeCellule {Libre, Occupee, Muree}

    // État actuel de la case
    private EtatDeCellule _propreEtat;

    // Permet d'accéder à la variable hors de la classe
    public EtatDeCellule Etat
    {
        get { return _propreEtat; }
        set { _propreEtat = value; }
    }

    // Variable permettant de ramener à la couleur par défaut en cas de besoin
    public Color _couleurParDefaut;

    // On enregistre la couleur initiale de l'objet
    void Awake()
    {
        _couleurParDefaut = GetComponent<MeshRenderer>().material.color;
    }
}
