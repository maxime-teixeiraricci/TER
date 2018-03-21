using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestUnitBehaviour : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        List<Instruction> behavior = new List<Instruction>(){
            new Instruction(new string[] { "PERCEPT_BASE_NEAR_ALLY", "PERCEPT_BAG_NOT_EMPTY"}, "ACTION_GIVE_RESSOURCE"),
            new Instruction(new string[] { "PERCEPT_BAG_FULL"}, "ACTION_BACK_TO_BASE"),
            new Instruction(new string[] { "PERCEPT_LIFE_NOT_MAX","PERCEPT_BAG_NOT_EMPTY"}, "ACTION_HEAL"),
            new Instruction(new string[] { "PERCEPT_BAG_NOT_FULL", "PERCEPT_FOOD_NEAR" }, "ACTION_PICK"),
            new Instruction(new string[] { "PERCEPT_BLOCKED" }, "ACTION_RANDOM_MOVE"),
            new Instruction(new string[] { }, "ACTION_MOVE") };
        string gamePath = "./teams/" + GetComponent<GameManager>()._gameName + "/";

        string teamName = GetComponent<TeamManager>()._teams[0]._name;
        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();
        interpreter.behaviorToXml(teamName, gamePath, "Explorer", behavior);


        behavior = new List<Instruction>(){
            new Instruction(new string[] { "PERCEPT_LIFE_NOT_MAX","PERCEPT_BAG_NOT_EMPTY"}, "ACTION_HEAL"),
            new Instruction(new string[] { "PERCEPT_IS_NOT_RELOADED" }, "ACTION_RELOAD"),
            new Instruction(new string[] { "PERCEPT_IS_RELOADED", "PERCEPT_ENEMY" }, "ACTION_FIRE"),
            new Instruction(new string[] { "PERCEPT_BAG_NOT_FULL", "PERCEPT_FOOD_NEAR" }, "ACTION_PICK"),
            new Instruction(new string[] { "PERCEPT_BLOCKED" }, "ACTION_RANDOM_MOVE"),
            new Instruction(new string[] { }, "ACTION_MOVE") };
        interpreter.behaviorToXml(teamName, gamePath, "Light", behavior);
        interpreter.behaviorToXml(teamName, gamePath, "Heavy", behavior);

        behavior = new List<Instruction>(){
            new Instruction(new string[] { "PERCEPT_LIFE_NOT_MAX","PERCEPT_BAG_NOT_EMPTY"}, "ACTION_HEAL"),
            new Instruction(new string[] { "PERCEPT_BAG_25"}, "ACTION_CREATE_HEAVY"),
        new Instruction(new string[] { "PERCEPT_BAG_10"}, "ACTION_CREATE_LIGHT")};
        interpreter.behaviorToXml(teamName, gamePath, "Base", behavior);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
