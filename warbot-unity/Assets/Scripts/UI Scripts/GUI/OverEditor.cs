using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverEditor : MonoBehaviour {

    private void OnMouseOver()
    {
        createPuzzle _createPuzzle = GameObject.Find("EventSystem").GetComponent<createPuzzle>();
        if (Input.GetKeyDown("delete"))
        {
            _createPuzzle.Undo();
        }
    }
}
