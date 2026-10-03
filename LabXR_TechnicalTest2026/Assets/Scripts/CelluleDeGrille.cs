using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class CelluleDeGrille : MonoBehaviour
{
    public enum EtatDeCellule {Libre, Occupee, Muree}
    private EtatDeCellule _propreEtat;

    // Permet d'accéder à la variable hors de la classe
    public EtatDeCellule State
    {
        get { return _propreEtat; }
        set { _propreEtat = value; }
    }


}
