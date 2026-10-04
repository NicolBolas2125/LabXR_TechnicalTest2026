using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestionUIMenuPrincipal : MonoBehaviour
{
    // Référence vers le Slider qui contient la taille de la grille à générer
    [SerializeField]
    private Slider _tailleDeLaGrille;

    // Affichage de la valeur du slider avant qu'on le tranfert
    [SerializeField]
    private TextMeshProUGUI _Text;

    // Fonction quand on lance le jeu
    public void AuLancement()
    {
        TransfertInformation.instance.Taille = (int) _tailleDeLaGrille.value;
        SceneManager.LoadScene("Jeu");
    }

    // Update is called once per frame
    void Update()
    {
        _Text.text = _tailleDeLaGrille.value.ToString();
    }
}
