using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreatePuzzle : MonoBehaviour
{
    public GameObject puzzle;
    public string _label;
    public Vector3 positionInitial;

    public void create()
    {
        GameObject _puzzle = Instantiate(puzzle);
        if (_puzzle.GetComponent<ActionPuzzleScript>())
        {
            _puzzle.GetComponent<ActionPuzzleScript>().actionName = _label;
        }
        else if (_puzzle.GetComponent<CondPuzzleScript>())
        {
            _puzzle.GetComponent<CondPuzzleScript>().condName = _label;
        }
            _puzzle.transform.parent = GameObject.Find("Editeur").transform;
        _puzzle.GetComponent<RectTransform>().anchoredPosition = positionInitial;
    }

}
