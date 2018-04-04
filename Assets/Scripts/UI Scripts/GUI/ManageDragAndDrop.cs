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

    public Vector3 _initialPosition;
    public float deltaDest = 10;
    public bool overEditor;


    private void OnMouseOver()
    {
        print("OK3");
        if (Input.GetMouseButtonDown(1) && !undestructible)
        {
            Destroy(gameObject);
        }
        if (Input.GetMouseButtonDown(0))
        {
            _initialPosition = Input.mousePosition;
        }
    }


    public void OnMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _initialPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && Vector3.Distance(_initialPosition, Input.mousePosition ) > deltaDest)
        {
            // Largeur et hauteur de l'éditeur de comportement
            /*float widthEditor = rendEditeur.bounds.size.x;
            float heightEditor = rendEditeur.bounds.size.y;*/
            float widthEditor = GameObject.Find("Editeur").GetComponent<RectTransform>().rect.width;
            float heightEditor = GameObject.Find("Editeur").GetComponent<RectTransform>().rect.height;

            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.parent = GameObject.Find("Editeur").transform;
            transform.position = objPosition;
            GetComponent<RectTransform>().localPosition = new Vector3(GetComponent<RectTransform>().localPosition.x, GetComponent<RectTransform>().localPosition.y, 10);


            float newX = Mathf.Min(Mathf.Max(GetComponent<RectTransform>().localPosition.x, -widthEditor / 2), widthEditor / 2);
            float newY = Mathf.Min(Mathf.Max(GetComponent<RectTransform>().localPosition.y, -heightEditor / 2), heightEditor / 2);
            Vector3 newPos = new Vector3(newX, newY, 10);
            GetComponent<RectTransform>().localPosition = newPos;
        }

    }

    public void OnMouseUp()

    {
        print("UP");
        float widthPuzzle = sizePuzzleX;
        float heightPuzzle = sizePuzzleY;


        // Largeur et hauteur de l'éditeur de comportement
        float widthEditor = GameObject.Find("Editeur").GetComponent<RectTransform>().rect.width;
        float heightEditor = GameObject.Find("Editeur").GetComponent<RectTransform>().rect.height;
        transform.parent = GameObject.Find("Editeur").transform;

        float minX = -widthEditor / 2;
        float maxX = widthEditor / 2;
        float minY = -heightEditor / 2;
        float maxY = heightEditor / 2;

        posGridX = (int)Mathf.Round(GetComponent<RectTransform>().localPosition.x / widthPuzzle);
        posGridY = (int)Mathf.Round(GetComponent<RectTransform>().localPosition.y / heightPuzzle);
        float newX = posGridX * widthPuzzle;
        float newY = posGridY * heightPuzzle;
        Vector3 newPos = new Vector3(newX, newY, 10);

        GetComponent<RectTransform>().localPosition = newPos;

    }

    public bool isRight(GameObject other)
    {
        return posGridX + posGridW == other.GetComponent<ManageDragAndDrop>().posGridX;
    }

    public void setGridPosition(Vector2 p)
    {
        posGridX = (int)p.x;
        posGridY = (int)p.y;
        GetComponent<RectTransform>().localPosition = new Vector3(posGridX * sizePuzzleX, posGridY * sizePuzzleY, 10);
    }
    public Vector2 getGridPosition()
    {
        return new Vector2(posGridX, posGridY);
    }

}