using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreateActionPuzzle : MonoBehaviour
{
    public GameObject actionPuzzle;
    public Vector3 positionInitial;

    public void create()
    {
        GameObject puzzle = Instantiate(actionPuzzle);
        puzzle.transform.parent = GameObject.Find("GameEditorScreen").transform;
        puzzle.GetComponent<RectTransform>().anchoredPosition = positionInitial;
    }

}
