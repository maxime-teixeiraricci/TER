using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamAndUnit : MonoBehaviour
{

    //public GameObject dropdownTeamLabel;
    //public GameObject dropdownUnitLabel;

    //find your dropdown menu transform
    public Transform dropdownMenuTeam;
    public Transform dropdownMenuUnit;
    public string valueTeam;
    public string valueUnit;

    void Start()
    {

    }



    void Update()
    {
        //find the selected index
        int menuIndexTeam = dropdownMenuTeam.GetComponent<Dropdown>().value;
        int menuIndexUnit = dropdownMenuUnit.GetComponent<Dropdown>().value;

        //find all options available within the dropdown menu
        List<Dropdown.OptionData> menuOptionsTeam = dropdownMenuTeam.GetComponent<Dropdown>().options;
        List<Dropdown.OptionData> menuOptionsUnit = dropdownMenuUnit.GetComponent<Dropdown>().options;

        //get the string value of the selected index
        valueTeam = menuOptionsTeam[menuIndexTeam].text;
        valueUnit = menuOptionsUnit[menuIndexUnit].text;
    }
}
