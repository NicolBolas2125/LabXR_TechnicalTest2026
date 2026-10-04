using UnityEngine;

public class TransfertInformation : MonoBehaviour
{
    // Référence unique vers cette classe
    static TransfertInformation _instance;

    // Moyen d'accès hors de la classe
    public static TransfertInformation instance
    {
        get
        {
            return _instance;
        }
    }


    // Ensemble des variables que l'on cherche à garder (en privé) et leur moyen d'accès (public)
    private int _tailleGrille;
    // Moyen d'obtenir les cibles hors de la classe
    public int Taille
    {
        get {return _tailleGrille;}
        set {_tailleGrille = value;}
    }

    // On définie une référence unique pour accéder aux dimensions et à l'état des cases dans d'autres scripts
    void Awake()
    {
        if (instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
}
