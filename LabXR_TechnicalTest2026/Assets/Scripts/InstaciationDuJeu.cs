using UnityEngine;

public class InstaciationDuJeu : MonoBehaviour
{
    // Référence vers le pion
    [SerializeField]
    private GameObject _pion;

    // Référence vers le générateur de grille
    [SerializeField]
    private GameObject _generateurGrille;

    // Référence vers la gestion des contrôles du jeu
    [SerializeField]
    private GameObject _souris;

    // Instancie tous les objets dont on a besoin
    void Awake()
    {
        Instantiate(_pion);
        Instantiate(_generateurGrille);
        Instantiate(_souris);
    }
}
