using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;

public class PositionSouris : MonoBehaviour
{
    private AEtoile calculChemin;

    [SerializeField]
    private Vector2[] chemin;


    // Awake sert à initialiser les variables utiles
    void Awake()
    {
        calculChemin = GetComponent<AEtoile>();
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

                // On colorie cet objet en bleu
                ObjetSelectionne.GetComponent<MeshRenderer>().material.color = Color.blue;

                // On cherche à créer un chemin le menant à 0,0
                calculChemin.Cible = new Vector2((int) ObjetSelectionne.transform.position.x, (int) ObjetSelectionne.transform.position.z);
                chemin = calculChemin.Aetoile(0, 0);
                GenerateurDeGrille grille = GenerateurDeGrille.instance;
                foreach (var element in chemin)
                {
                    grille._grille[(int) element.x, (int) element.y].GetComponent<MeshRenderer>().material.color = Color.yellow;
                }
                
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

                // On colorie cet objet en rouge
                ObjetSelectionne.GetComponent<MeshRenderer>().material.color = Color.red;

                // On verrouille cette case avec un mur
                GenerateurDeGrille.instance._grille[ (int) ObjetSelectionne.transform.position.x, (int) ObjetSelectionne.transform.position.z].Etat = CelluleDeGrille.EtatDeCellule.Muree;
            }
        }
    }
}
