using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridEditor : MonoBehaviour
{

    float widthEditor;
    float heightEditor;
    int widthGrid;
    int heightGrid;
    public int[,] grid;



    // Use this for initialization
    public void Start()
    {

        gridInitializer();

    }


    public void gridInitializer()
    {

        Renderer rendEditeur = GameObject.Find("Editeur").GetComponent<Renderer>();

        widthEditor = rendEditeur.bounds.size.x;
        heightEditor = rendEditeur.bounds.size.y;

        widthGrid = (int)(widthEditor / 185);
        heightGrid = (int)(heightEditor / 80);

        grid = new int[widthGrid, heightGrid];

        // Debug.Log("widthEditor = " + widthEditor);
        // Debug.Log("heightEditor = " + heightEditor);
        // Debug.Log("(widthEditor / 185 = " + (int)(widthEditor / 185));
        // Debug.Log("(heightEditor / 80) = " + (int)(heightEditor / 80));


        for (int i = 0; i < widthGrid; i++)
        {
            for (int j = 0; j < heightGrid; j++)
            {
                grid[i, j] = 0;
            }
        }
    }



    public void gridConstructor()
    {
        
            for (int i = 0; i < widthGrid; i++)
            {
                for (int j = 0; j < heightGrid; j++)
                {
                    if (grid[i, j] != 0)
                    {
                        
                    }
                }
            }
        
    }

}