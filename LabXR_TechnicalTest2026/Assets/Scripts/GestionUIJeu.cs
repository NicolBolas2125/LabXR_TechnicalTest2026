using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionUIJeu : MonoBehaviour
{


    // Affichage de la valeur du slider avant qu'on le tranfert
    [SerializeField]
    private TextMeshProUGUI _Text;

    // Fonction quand on veut retourner au menu
    public void Retour()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    // Fonction quand on veut réinitialiser la grille
    public void Reinitialiser()
    {
        // On récupère la grille
        CelluleDeGrille[,] grille = GenerateurDeGrille.instance._grille;
        // Pour chaque case, on la rend libre si elle est murée
        foreach (var element in grille)
        {
            if (element.Etat == CelluleDeGrille.EtatDeCellule.Muree)
            {
                element.GetComponent<MeshRenderer>().material.color = element._couleurParDefaut;
                element.Etat = CelluleDeGrille.EtatDeCellule.Libre;
            }
        }
    }
}
