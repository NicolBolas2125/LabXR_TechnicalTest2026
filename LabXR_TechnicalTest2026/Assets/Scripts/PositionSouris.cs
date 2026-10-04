using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

public class PositionSouris : MonoBehaviour
{
    private AEtoile _calculChemin;

    [SerializeField]
    private Vector2[] chemin;

    // Référence vers le pion
    private MouvementPion _pion;



    // Awake sert à initialiser les variables utiles
    void Awake()
    {
        _calculChemin = GetComponent<AEtoile>();
    }

    // On récupère l'instance dans Start() et non dans Awake() car elle est exécutée après (et donc le Awake de l'autre classe a le temps de bien définir l'instance)
    void Start()
    {
        _pion = MouvementPion.instance;
    }
    // Update is called once per frame
    void Update()
    {
        // On vérifie si la souris a été cliquée (clic gauche)
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // On projete le rayon pour voir si on a cliqué sur une case
		    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		    RaycastHit touche;
            if( Physics.Raycast(ray, out touche) ){
                // On récupère l'objet sélectionné
                GameObject ObjetSelectionne = touche.collider.gameObject;

                _calculChemin.Cible = new Vector2((int) ObjetSelectionne.transform.position.x, (int) ObjetSelectionne.transform.position.z);
                

                if (_pion._cibleActuelle == -1)
                {
                    chemin = _calculChemin.Aetoile(Mathf.RoundToInt(_pion.transform.position.x), Mathf.RoundToInt(_pion.transform.position.z));
                }
                else
                {
                    chemin = _calculChemin.Aetoile((int) _pion.Cibles[_pion._cibleActuelle].x, (int) _pion.Cibles[_pion._cibleActuelle].z);
                }

                _pion.nouveauTrajet(chemin);
                
            }
        }

        // On vérifie si la souris a été cliquée (clic droit)
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            // On projete le rayon pour voir si on a cliqué sur une case
		    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		    RaycastHit touche;
            if( Physics.Raycast(ray, out touche) ){
                // On récupère l'objet sélectionné
                GameObject ObjetSelectionne = touche.collider.gameObject;
                if (ObjetSelectionne.GetComponent<CelluleDeGrille>().Etat == CelluleDeGrille.EtatDeCellule.Libre)
                {

                    // On colorie cet objet en rouge
                    ObjetSelectionne.GetComponent<MeshRenderer>().material.color = Color.red;

                    // On verrouille cette case avec un mur
                    GenerateurDeGrille.instance._grille[ (int) ObjetSelectionne.transform.position.x, (int) ObjetSelectionne.transform.position.z].Etat = CelluleDeGrille.EtatDeCellule.Muree;

                    // On recalcule le chemin si le pion est en train de bouger
                    if (_pion._cibleActuelle != -1)
                    {
                        chemin = _calculChemin.Aetoile((int) _pion.Cibles[_pion._cibleActuelle].x, (int) _pion.Cibles[_pion._cibleActuelle].z);
                        _pion.nouveauTrajet(chemin);
                    }
                }
                else
                {
                    // On vérifie si la case est déjjà murée
                    if (ObjetSelectionne.GetComponent<CelluleDeGrille>().Etat == CelluleDeGrille.EtatDeCellule.Muree)
                    {
                        Debug.Log("Déjà murée");
                    }
                    // Elle est occupée
                    else
                    {
                        Debug.Log("Case Occupée");
                    }
                }
            }
        }
    }
}
