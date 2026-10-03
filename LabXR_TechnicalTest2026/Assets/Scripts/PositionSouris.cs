using UnityEngine;
using UnityEngine.InputSystem;

public class PositionSouris : MonoBehaviour
{
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
            }
        }
    }
}
