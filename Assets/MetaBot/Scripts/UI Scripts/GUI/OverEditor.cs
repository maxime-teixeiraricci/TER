using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverEditor : MonoBehaviour {

    // supprime une pièce si le curseur est dessus, et que la touche "SUPPR" est pressée
    private void OnMouseOver()
    {
        createPuzzle _createPuzzle = GameObject.Find("EventSystem").GetComponent<createPuzzle>();
        if (Input.GetKeyDown("delete"))
        {
            _createPuzzle.Undo();
        }
    }
}
