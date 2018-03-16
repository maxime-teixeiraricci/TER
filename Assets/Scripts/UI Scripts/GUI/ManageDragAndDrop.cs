using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageDragAndDrop : MonoBehaviour
{
    public int posGridX;
    public int posGridY;
    public int posGridH;
    public int posGridW;

    public float sizePuzzleX;
    public float sizePuzzleY;
    public bool undestructible;
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

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && !undestructible)
        {
            Destroy(gameObject);
        }
    }


    public void OnMouseDrag()
    {
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

        float newX = Mathf.Min(Mathf.Max(GetComponent<RectTransform>().localPosition.x, -widthEditor / 2), widthEditor / 2);
        float newY = Mathf.Min(Mathf.Max(GetComponent<RectTransform>().localPosition.y, -heightEditor / 2), heightEditor / 2);
        Vector3 newPos = new Vector3(newX, newY, transform.position.z);
        GetComponent<RectTransform>().localPosition = newPos;

        /*
        float currentPositionX = transform.position.x + widthPuzzle / 2;
        float maxPositionX = widthCanvas - widthPuzzle;
        float minPositionX = widthControlPanel + widthPuzzle / 2;

    

        float currentPositionY = transform.position.y - heightPuzzle / 2;
        float minPositionY = heightPuzzle;
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
        }*/
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
        /*float widthPuzzle = collider2D.bounds.size.x;
        float heightPuzzle = collider2D.bounds.size.y;*/

        float widthPuzzle = sizePuzzleX;
        float heightPuzzle = sizePuzzleY;


        // Largeur et hauteur de l'éditeur de comportement
        float widthEditor = rendEditeur.bounds.size.x;
        float heightEditor = rendEditeur.bounds.size.y;

        // Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        // Vector3 trueMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //y = 100
        //x = -271 - -83 = 
        float minX = -widthEditor / 2;
        float maxX = widthEditor / 2;
        float minY = -heightEditor/2;
        float maxY = heightEditor/2;
        
        posGridX = (int)Mathf.Round(currentObject.GetComponent<RectTransform>().localPosition.x  / widthPuzzle);
        posGridY = (int)Mathf.Round(currentObject.GetComponent<RectTransform>().localPosition.y / heightPuzzle);
        float newX = posGridX * widthPuzzle;
        float newY = posGridY * heightPuzzle;
        Vector3 newPos = new Vector3(newX, newY, transform.position.z);

        currentObject.GetComponent<RectTransform>().localPosition = newPos;



















        /*


        // transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        // 1;5;9;13;17;21
        if ( currentObject.GetComponent<RectTransform>().localPosition.x > -365 && currentObject.GetComponent<RectTransform>().localPosition.x < -177)
            {       // 1
                    if ( currentObject.GetComponent<RectTransform>().localPosition.y < 286 && currentObject.GetComponent<RectTransform>().localPosition.y > 186)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(-271, 236, transform.position.z); 
                    }
                    // 5
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < 186 && currentObject.GetComponent<RectTransform>().localPosition.y > 86)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(-271, 136, transform.position.z);
                    }
                    // 9
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < 86 && currentObject.GetComponent<RectTransform>().localPosition.y > -14)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(-271, 36, transform.position.z);
                    }
                    // 13
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < -14 && currentObject.GetComponent<RectTransform>().localPosition.y > -114)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(-271, -64, transform.position.z);
                    }
                    // 17
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < -114 && currentObject.GetComponent<RectTransform>().localPosition.y > -214)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(-271, -164, transform.position.z);
                    }
                    // 21
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < -214 && currentObject.GetComponent<RectTransform>().localPosition.y > -314)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(-271, -264, transform.position.z);
                    }

            }

            // 2;6;10;14;18;22
            else if (currentObject.GetComponent<RectTransform>().localPosition.x > -177 && currentObject.GetComponent<RectTransform>().localPosition.x < 11)
            {
                    // 2
                    if (currentObject.GetComponent<RectTransform>().localPosition.y < 286 && currentObject.GetComponent<RectTransform>().localPosition.y > 186)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(-83, 236, transform.position.z);
                    }
                    // 6
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < 186 && currentObject.GetComponent<RectTransform>().localPosition.y > 86)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(-83, 136, transform.position.z);
                    }
                    // 10
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < 86 && currentObject.GetComponent<RectTransform>().localPosition.y > -14)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(-83, 36, transform.position.z);
                    }
                    // 14
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < -14 && currentObject.GetComponent<RectTransform>().localPosition.y > -114)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(-83, -64, transform.position.z);
                    }
                    // 18
                    else  if (currentObject.GetComponent<RectTransform>().localPosition.y < -114 && currentObject.GetComponent<RectTransform>().localPosition.y > -214)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(-83, -164, transform.position.z);
                    }
                    // 22
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < -214 && currentObject.GetComponent<RectTransform>().localPosition.y > -314)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(-83, -264, transform.position.z);
                    }
            }
            // 3;7;11;15;19;23
            else if (currentObject.GetComponent<RectTransform>().localPosition.x > 11 && currentObject.GetComponent<RectTransform>().localPosition.x < 199)
            {
                    // 3
                    if (currentObject.GetComponent<RectTransform>().localPosition.y < 286 && currentObject.GetComponent<RectTransform>().localPosition.y > 186)
                    {
                       currentObject.GetComponent<RectTransform>().localPosition = new Vector3(105, 236, transform.position.z);
                    }
                    // 7
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < 186 && currentObject.GetComponent<RectTransform>().localPosition.y > 86)
                    {
                       currentObject.GetComponent<RectTransform>().localPosition = new Vector3(105, 136, transform.position.z);
                    }
                    // 11
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < 86 && currentObject.GetComponent<RectTransform>().localPosition.y > -14)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(105, 36, transform.position.z);
                    }
                    // 15
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < -14 && currentObject.GetComponent<RectTransform>().localPosition.y > -114)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(105, -64, transform.position.z);
                    }
                    // 19
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < -114 && currentObject.GetComponent<RectTransform>().localPosition.y > -214)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(105, -164, transform.position.z);
                    }
                    // 23
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < -214 && currentObject.GetComponent<RectTransform>().localPosition.y > -314)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(105, -264, transform.position.z);
                    }
            }
            // 4;8;12;16;20;24
            else if (currentObject.GetComponent<RectTransform>().localPosition.x > 199 && currentObject.GetComponent<RectTransform>().localPosition.x < 387)
            {
                    // 4
                    if (currentObject.GetComponent<RectTransform>().localPosition.y < 286 && currentObject.GetComponent<RectTransform>().localPosition.y > 186)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(293, 236, transform.position.z);
                    }
                    // 8
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < 186 && currentObject.GetComponent<RectTransform>().localPosition.y > 86)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(293, 136, transform.position.z);
                    }
                    // 12
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < 86 && currentObject.GetComponent<RectTransform>().localPosition.y > -14)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(293, 36, transform.position.z);
                    }
                    // 16
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < -14 && currentObject.GetComponent<RectTransform>().localPosition.y > -114)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(293, -64, transform.position.z);
                    }
                    // 20
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < -114 && currentObject.GetComponent<RectTransform>().localPosition.y > -214)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(293, -164, transform.position.z);
                    }
                    // 24
                    else if (currentObject.GetComponent<RectTransform>().localPosition.y < -214 && currentObject.GetComponent<RectTransform>().localPosition.y > -314)
                    {
                        currentObject.GetComponent<RectTransform>().localPosition = new Vector3(293, -264, transform.position.z);
                    }
            }
            else transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            */
    }

    public bool isRight(GameObject other)
    {
        return posGridX + posGridW == other.GetComponent<ManageDragAndDrop>().posGridX;
    }

}