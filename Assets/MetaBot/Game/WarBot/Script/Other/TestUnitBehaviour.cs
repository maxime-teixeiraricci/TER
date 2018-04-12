using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestUnitBehaviour : MonoBehaviour
{

	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateDefaultBehaviour()
    {
        Debug.ClearDeveloperConsole();
        Debug.Log("Creating Default Team ...");
        List<Instruction> behavior = new List<Instruction>(){
            new Instruction(new string[] { "PERCEPT_ENEMY"},new MessageStruct[] {new MessageStruct("ACTN_MESSAGE_HELP", "Light") }, "ACTION_MOVE"),
            new Instruction(new string[] { "PERCEPT_BLOCKED" }, "ACTION_MOVE_UNTIL_UNBLOCKED"),
            new Instruction(new string[] { "PERCEPT_BASE_NEAR_ALLY", "PERCEPT_BAG_NOT_EMPTY"}, "ACTION_GIVE_RESSOURCE"),
            new Instruction(new string[] { "PERCEPT_BAG_FULL"}, "ACTION_BACK_TO_BASE"),
            new Instruction(new string[] { "PERCEPT_LIFE_NOT_MAX","PERCEPT_BAG_NOT_EMPTY"}, "ACTION_HEAL"),
            new Instruction(new string[] { "PERCEPT_BAG_NOT_FULL", "PERCEPT_FOOD_NEAR" }, "ACTION_PICK"),
            new Instruction(new string[] { "PERCEPT_BLOCKED" }, "ACTION_MOVE_UNTIL_UNBLOCKED"),
            new Instruction(new string[] { },"ACTION_MOVE") };
        string gamePath = Application.streamingAssetsPath + "/teams/" + GetComponent<GameManager>()._gameName + "/";

        string teamName = "Default Team";
        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();
        interpreter.behaviorToXml(teamName, gamePath, "Explorer", behavior);
        

        behavior = new List<Instruction>(){
           new Instruction(new string[] { "PERCEPT_IS_RELOADED", "PERCEPT_CONTRACT", "CONTRACT_ELIMINATION_TARGET_NEAR"}, "ACTION_FIRE"),
           new Instruction(new string[] {"PERCEPT_MESSAGE_ATTACK"}, new MessageStruct[] {new MessageStruct("ACTN_ADD_ELIMINATION_CONTRACT", "None") }, "ACTION_MOVE"),
           new Instruction(new string[] { "PERCEPT_LIFE_NOT_MAX","PERCEPT_BAG_NOT_EMPTY"}, "ACTION_HEAL"),
            new Instruction(new string[] { "PERCEPT_IS_NOT_RELOADED" }, "ACTION_RELOAD"),
            //new Instruction(new string[] { "PERCEPT_IS_RELOADED", "PERCEPT_ENEMY" }, "ACTION_FIRE"),
            new Instruction(new string[] { "PERCEPT_BAG_NOT_FULL", "PERCEPT_FOOD_NEAR" }, "ACTION_PICK"),
            new Instruction(new string[] { "PERCEPT_BLOCKED" }, "ACTION_MOVE_UNTIL_UNBLOCKED"),
            new Instruction(new string[] { }, "ACTION_MOVE")};
        interpreter.behaviorToXml(teamName, gamePath, "Light", behavior);
        interpreter.behaviorToXml(teamName, gamePath, "Heavy", behavior);

        behavior = new List<Instruction>(){
            new Instruction(new string[] { "PERCEPT_LIFE_NOT_MAX","PERCEPT_BAG_NOT_EMPTY"}, "ACTION_HEAL"),
            new Instruction(new string[] { "PERCEPT_BAG_10"}, "ACTION_CREATE_LIGHT") };
        interpreter.behaviorToXml(teamName, gamePath, "Base", behavior);
        Debug.Log("Creating Default Team DONE!");
    }
}
