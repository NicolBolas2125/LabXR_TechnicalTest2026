using UnityEngine;
using UnityEngine.InputSystem;

public class PositionSouris : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
		    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		    RaycastHit touche;
            if( Physics.Raycast(ray, out touche) ){
                GameObject ObjetSelectione = touche.collider.gameObject;

                ObjetSelectione.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
        }
    }
}
