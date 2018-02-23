using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageDragAndDrop : MonoBehaviour {

    float distance = 10;

    // TODO
    // Essayer de récupérer la string de l'objet sélectionné par la souris, et le passer en argument de Find("String s"), pour utiliser ce script pour tous les objets
    public void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }
}