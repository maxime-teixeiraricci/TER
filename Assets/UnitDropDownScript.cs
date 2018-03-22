using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDropDownScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        
        Dropdown _Dropdown = GetComponent<Dropdown>();
        _Dropdown.ClearOptions();
        List<string> _DropOptions = new List<string>();
        foreach (UnitPerceptAction unit in GameObject.Find("EditorManager").GetComponent<EditorManagerScript>()._unitBehaviour)
        {
            _DropOptions.Add(unit.unit);
        }
        _Dropdown.AddOptions(_DropOptions);
    }
	
	// Update is called once per frame
	void Update ()
    {
     
    }

  
}
