using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropPuzzleMenu : MonoBehaviour
{
    public Vector3 _initialPosition;
    public Vector3 _initialMousePosition;
    public float distance;
    public float speed;


    /*
    public static ArrayList recoverList = new ArrayList();
    public static int cptObjects;
    public static int cptUndo;
    public static GameObject pieceToUndo;
    public static GameObject pieceToRedo;*/





    public void OnMouseDown()
    {

    }

    private void OnMouseOver()
    {

    }


    public void OnMouseDrag()
    {


        // Largeur et hauteur de l'éditeur de comportement
        /*float widthEditor = rendEditeur.bounds.size.x;
        float heightEditor = rendEditeur.bounds.size.y;*/
        float widthEditor = transform.parent.GetComponent<RectTransform>().rect.width;
        float heightEditor = transform.parent.GetComponent<RectTransform>().rect.height;

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;
        GetComponent<RectTransform>().localPosition = new Vector3(GetComponent<RectTransform>().localPosition.x, GetComponent<RectTransform>().localPosition.y, -1);

        float newX = Mathf.Min(Mathf.Max(GetComponent<RectTransform>().localPosition.x, -widthEditor / 2), widthEditor / 2);
        float newY = Mathf.Min(Mathf.Max(GetComponent<RectTransform>().localPosition.y, -heightEditor / 2), heightEditor / 2);
        Vector3 newPos = new Vector3(newX, newY, -1);
        GetComponent<RectTransform>().localPosition = newPos;

    }

}