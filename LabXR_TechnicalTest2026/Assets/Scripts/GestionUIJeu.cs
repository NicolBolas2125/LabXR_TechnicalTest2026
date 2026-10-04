using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionUIJeu : MonoBehaviour
{
    // Référence unique vers cette classe
    static GestionUIJeu _instance;

    // Moyen d'accès hors de la classe
    public static GestionUIJeu instance
    {
        get
        {
            return _instance;
        }
    }
    
    // Texte de retour pour l'utilisateur
    [SerializeField]
    private GameObject _texteDeFeedback;

    
    // coroutine pour le retour utilisateur
    private IEnumerator RetourUtilisateur(string message)
    {
        _texteDeFeedback.GetComponentInChildren<TextMeshProUGUI>().text = message;
        _texteDeFeedback.SetActive(true);
        yield return new WaitForSeconds(1f);
        _texteDeFeedback.SetActive(false);
    }

    // Fonction quand on veut retourner au menu
    public void Retour()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    // Fonction quand on veut réinitialiser la grille
    public void Reinitialiser()
    {
        callRetourUtilisateur("Réinitialisation des murs");
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

    public void callRetourUtilisateur(string message)
    {
        StartCoroutine(RetourUtilisateur(message));
    }

    // On initialise nos variables de texte
    void Awake()
    {
        _texteDeFeedback.SetActive(false);
        if (instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
