using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageDragAndDrop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    string s = "Condition1";

    // TODO
    // Essayer de récupérer la string de l'objet sélectionné par la souris, et le passer en argument de Find("String s"), pour utiliser ce script pour tous les objets
    public void Drag()
    {
        GameObject.Find(s).transform.position = Input.mousePosition;
    }


    // TODO
    // Rendre la fonction plus générique, comme pour Drag(), pour gérer les différents "placeHolders"
    public void Drop()
    {
        GameObject ph1 = GameObject.Find("placeHolder");
        float distance = Vector3.Distance(GameObject.Find(s).transform.position, ph1.transform.position);
        if (distance < 50)
        {
            GameObject.Find(s).transform.position = ph1.transform.position;
        }
    }
}
