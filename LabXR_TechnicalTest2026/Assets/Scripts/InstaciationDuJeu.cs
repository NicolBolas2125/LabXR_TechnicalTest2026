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

    private Camera _mainCamera;

    // Instancie tous les objets dont on a besoin
    void Awake()
    {
        Instantiate(_pion);
        Instantiate(_generateurGrille);
        Instantiate(_souris);
        _mainCamera = Camera.main;

    }

    // Pour centrer la caméra proprement sur le centre de la grille
    void Start()
    {
        // On récupère la taille du tableau pour centrer la caméra
        float taille = TransfertInformation.instance.Taille;
        _mainCamera.transform.position = new Vector3(taille / 2, taille, - taille / 2);
    }
}
