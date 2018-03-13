using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPuzzleScript : MonoBehaviour {
    public string actionName;
    public Text _labelText;

    void Update()
    {
        _labelText.GetComponent<Text>().text = actionName;
    }
}
