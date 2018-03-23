using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUnit : ActionCommon
{

    void Start()
    {
        InitAction();
    }

    public override void InitAction()
    {
        base.InitAction(); // IMPORTANT : Permet de recuperer les percepts de la classe mere
        _actions["ACTION_MOVE"] = delegate () { GetComponent<MovableCharacter>().Move(); };
        _actions["ACTION_RANDOM_MOVE"] = delegate ()
        {
            GetComponent<Stats>()._heading = Random.Range(0f, 360f);
            _actions["ACTION_MOVE"]();
        };
        _actions["ACTION_TURN_AROUND_MOVE"] = delegate ()
        {
            GetComponent<Stats>()._heading += Random.Range(90f, 270f);
            _actions["ACTION_MOVE"]();
        };
        _actions["ACTION_HEAL"] = delegate () { GetComponent<Inventory>().use("Ressource"); };
        _actions["ACTION_IDLE"] = delegate () { };
        _actions["ACTION_PICK"] = delegate () {
            GameObject target = GetComponent<Stats>()._target;
            if (target != null)
            {
                Objet obj = target.GetComponent<ItemHeldler>()._heldObjet;
                Inventory unitInventory = GetComponent<Inventory>();
                unitInventory.add(obj);
                Destroy(target);
            }
        };
        _actions["ACTION_BACK_TO_BASE"] = delegate () {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Unit"))
            {
                if (go.GetComponent<Stats>()._unitType == "Base" && go.GetComponent<Stats>()._teamIndex == GetComponent<Stats>()._teamIndex)
                {
                    float a = Utility.getAngle(gameObject, go);
                    GetComponent<Stats>()._heading = a;
                    GetComponent<MovableCharacter>().Move();
                    break;
                }
            }
        };
        _actions["ACTION_CREATE_LIGHT"] = delegate () {
            print("OK");
            Objet o = GetComponent<Inventory>().find("Ressource");

            GetComponent<Inventory>()._objets[o] -= 10;
            GetComponent<CreatorUnit>().Create("Light");
        };
        _actions["ACTION_CREATE_HEAVY"] = delegate () {
            print("OK");
            Objet o = GetComponent<Inventory>().find("Ressource");
            GetComponent<Inventory>()._objets[o] -= 25;
            GetComponent<CreatorUnit>().Create("Heavy");
        };
    }
}
