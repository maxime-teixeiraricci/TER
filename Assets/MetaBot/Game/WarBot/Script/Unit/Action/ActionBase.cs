using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBase : ActionCommon
{

    void Start()
    {
        InitAction();
        forTheStart();
    }

    public override void InitAction()
    {
        base.InitAction(); // IMPORTANT : Permet de recuperer les percepts de la classe mere
        _actions["ACTION_CREATE_LIGHT"] = delegate () {
            Objet o = GetComponent<Inventory>().find("Ressource");
            if (GetComponent<Inventory>()._objets[o] >= 10)
            {
                GetComponent<Inventory>()._objets[o] -= 10;
                GetComponent<CreatorUnit>().Create("Light");
            }
        };
        _actions["ACTION_CREATE_HEAVY"] = delegate () {
            print("OK");
            Objet o = GetComponent<Inventory>().find("Ressource");
            if (GetComponent<Inventory>()._objets[o] >= 25)
            {
                
                GetComponent<Inventory>()._objets[o] -= 25;
                GetComponent<CreatorUnit>().Create("Heavy");
            }
        };

        _actions["ACTION_CREATE_EXPLORER"] = delegate () {
            Objet o = GetComponent<Inventory>().find("Ressource");
            if (GetComponent<Inventory>()._objets[o] >= 10)
            {
                GetComponent<Inventory>()._objets[o] -= 10;
                GetComponent<CreatorUnit>().Create("Explorer");
            }
        };
    }

    public void forTheStart()
    {
        base.InitAction(); // IMPORTANT : Permet de recuperer les percepts de la classe mere
        int explorer = GameObject.Find("GameManager").GetComponent<SaveSliderVal>().valSliderExplorer;
        int heavy = GameObject.Find("GameManager").GetComponent<SaveSliderVal>().valSliderHeavy;
        int light = GameObject.Find("GameManager").GetComponent<SaveSliderVal>().valSliderLight;

        if( explorer < 0 ) explorer = 0;
        if( heavy < 0 ) heavy = 0;
        if( light < 0 ) light = 0;

        for( int i = 0 ; i < explorer ; i++ ) GetComponent<CreatorUnit>().Create("Explorer");
        for( int j = 0 ; j < heavy ; j++ ) GetComponent<CreatorUnit>().Create("Heavy");
        for( int k = 0 ; k < light ; k++ ) GetComponent<CreatorUnit>().Create("Light");
    }
}
