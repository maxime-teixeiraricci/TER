using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUnit : Action
{

    void Start()
    {
        InitAction();
    }

    public override void InitAction()
    {
        _actions["ACTION_MOVE"] = delegate () { GetComponent<MovableCharacter>().Move(); };
        _actions["ACTION_RANDOM_MOVE"] = delegate ()
        {
            GetComponent<Stats>()._heading = Random.Range(0, 360);
            GetComponent<MovableCharacter>().Move();
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
        _actions["ACTION_GIVE_RESSOURCE"] = delegate () {
            GameObject target = GetComponent<Stats>()._target;
            if (target != null)
            {
                Objet item = GetComponent<Inventory>().find("Ressource");
                if (GetComponent<Inventory>()._objets.ContainsKey(item) && GetComponent<Inventory>()._objets[item] != 0 && target.GetComponent<Inventory>())
                {
                    target.GetComponent<Inventory>().add(item);
                    GetComponent<Inventory>().pop(item);
                }
            }
        };
        _actions["ACTION_FIRE"] = delegate () {
            if (GetComponent<Stats>()._reloadTime <= 0)
            {
                GetComponent<Shooter>().Shoot();
            }
        };
        _actions["ACTION_RELOAD"] = delegate () { GetComponent<Stats>()._reloadTime -= Time.deltaTime; };
        _actions["ACTION_CREATE_LIGHT"] = delegate () {
            Objet o = GetComponent<Inventory>().find("Ressource");
            GetComponent<Inventory>()._objets[o] -= 10;
            GetComponent<CreatorUnit>().Create("Light");
        };

        _actions["ACTION_CREATE_HEAVY"] = delegate () {
            Objet o = GetComponent<Inventory>().find("Ressource");
            GetComponent<Inventory>()._objets[o] -= 25;
            GetComponent<CreatorUnit>().Create("Heavy");
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
    }
}
