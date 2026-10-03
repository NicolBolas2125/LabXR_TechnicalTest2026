using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

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
    public EtatDeCellule State
    {
        get { return _propreEtat; }
        set { _propreEtat = value; }
    }


}
