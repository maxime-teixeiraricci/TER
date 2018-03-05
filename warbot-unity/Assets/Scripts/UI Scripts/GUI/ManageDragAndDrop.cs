using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageDragAndDrop : MonoBehaviour {

    float distance = 10;
    int nbrObjects = 0;
    public static GameObject currentObject;

    /*
    public static ArrayList recoverList = new ArrayList();
    public static int cptObjects;
    public static int cptUndo;
    public static GameObject pieceToUndo;
    public static GameObject pieceToRedo;*/



    public void OnMouseDown()
    {
        // Nombre de pièces de puzzle présentes dans l'éditeur
        nbrObjects = createPuzzle.cptObjects;
        


    }

    public void OnMouseDrag()
    {
        createPuzzle _createPuzzle = GameObject.Find("EventSystem").GetComponent<createPuzzle>();
        Renderer rendEditeur = GameObject.Find("Editeur").GetComponent<Renderer>();
        Renderer rendCanvas = GameObject.Find("GameEditorScreen").GetComponent<Renderer>();
        Renderer rendControlPanel = GameObject.Find("Panneau controle").GetComponent<Renderer>();
        var collider2D = gameObject.GetComponent<Collider2D>();

        // Dimensions du canvas
        float widthCanvas = rendCanvas.bounds.size.x;
        float heightCanvas = rendCanvas.bounds.size.y;


        // Largeur et hauteur de la pièce de puzzle
        float widthPuzzle = collider2D.bounds.size.x;
        float heightPuzzle = collider2D.bounds.size.y;


        // Largeur et hauteur de l'éditeur de comportement
        float widthEditor = rendEditeur.bounds.size.x;
        float heightEditor = rendEditeur.bounds.size.y;

        // Largeur et hauteur du panneau de contrôle
        float widthControlPanel = rendControlPanel.bounds.size.x;
        float heightControlPanel = rendControlPanel.bounds.size.y;


        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
   
        float currentPositionX = transform.position.x + widthPuzzle / 2;
        float maxPositionX = widthCanvas - widthPuzzle;
        float minPositionX = widthControlPanel + widthPuzzle / 2;

    

        float currentPositionY = transform.position.y - heightPuzzle / 2;
        float minPositionY = 0 + heightPuzzle;
        float maxPositionY = heightEditor;

        Debug.Log("SOURIS X = " + currentPositionX);
        Debug.Log("SOURIS Y = " + currentPositionY);

        if (transform.position.x > maxPositionX)
            {
            transform.position = new Vector3(maxPositionX, transform.position.y, transform.position.z);
            }  

        if(transform.position.x + (widthPuzzle / 2) < minPositionX)
            {
            transform.position = new Vector3(minPositionX - (widthPuzzle / 2), transform.position.y, transform.position.z);
        }

        if(transform.position.y < minPositionY)
        {
            transform.position = new Vector3(transform.position.x, minPositionY, transform.position.z);
        }

        if(transform.position.y > maxPositionY)
        {
            transform.position = new Vector3(transform.position.x, maxPositionY, transform.position.z);
        }
        
            if (Input.GetKeyDown("delete"))
            {
                _createPuzzle.Undo();
            }
        

    }

    public void OnMouseUpAsButton()
    
    {
        currentObject = gameObject;
        Renderer rendEditeur = GameObject.Find("Editeur").GetComponent<Renderer>();
        Renderer rendCanvas = GameObject.Find("GameEditorScreen").GetComponent<Renderer>();
        Renderer rendControlPanel = GameObject.Find("Panneau controle").GetComponent<Renderer>();
        var collider2D = gameObject.GetComponent<Collider2D>();


        // Dimensions du canvas
        float widthCanvas = rendCanvas.bounds.size.x;
        float heightCanvas = rendCanvas.bounds.size.y;


        // Largeur et hauteur de la pièce de puzzle
        float widthPuzzle = collider2D.bounds.size.x;
        float heightPuzzle = collider2D.bounds.size.y;


        // Largeur et hauteur de l'éditeur de comportement
        float widthEditor = rendEditeur.bounds.size.x;
        float heightEditor = rendEditeur.bounds.size.y;

        // Largeur et hauteur du panneau de contrôle
        float widthControlPanel = rendControlPanel.bounds.size.x;
        float heightControlPanel = rendControlPanel.bounds.size.y;

        float currentPositionX = transform.position.x + widthPuzzle / 2;
        float maxPositionX = widthCanvas - widthPuzzle;
        float minPositionX = widthControlPanel + widthPuzzle / 2;


        float currentPositionY = transform.position.y - heightPuzzle / 2;
        float minPositionY = heightPuzzle;
        float maxPositionY = heightEditor;

        if (transform.position.x >= minPositionX - widthPuzzle || transform.position.x <= maxPositionX)
        {
            transform.position = new Vector3((maxPositionX / 4) +( widthPuzzle / 2), maxPositionY + heightPuzzle * (nbrObjects-1) * (-1), transform.position.z);
        }
    }

}