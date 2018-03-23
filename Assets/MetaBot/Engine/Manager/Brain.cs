using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(Sight))]
[RequireComponent(typeof(Inventory))]
public class Brain : MonoBehaviour
{

    public List<Instruction> _instructions = new List<Instruction>();
    public Percept _percepts;
    public Action _actions;
    private string _currentAction;
    public MessageManager _messageManager;
    public int nbInstruction;
    public int nbActions;

    void Start()
    {
        //GameObject.Find("Canvas").GetComponent<HUDManager>().CreateHUD(gameObject);
        GameObject.Find("Canvas").GetComponent<HUDManager>().CreateHUD(gameObject);
        if (GetComponent<Stats>()._teamIndex < GameObject.Find("GameManager").GetComponent<TeamManager>()._teams.Count)
        {
            _instructions = GameObject.Find("GameManager").GetComponent<TeamManager>().getUnitsBevahiours(GetComponent<Stats>()._teamIndex, GetComponent<Stats>()._unitType);
        }
        else
        {
            Destroy(gameObject);
        }

        _percepts = GetComponent<Percept>();
        _actions = GetComponent<Action>();
        //_messageManager = new MessageManager(this.gameObject);
        _messageManager = GetComponent<MessageManager>();
    }

    void FixedUpdate()
    {
        nbInstruction = _instructions.Count;
        if (_instructions != null && _actions != null && _actions._actions.Count != 0)
        {
            string _action = NextAction();
            print(gameObject + " " + _action);
            _actions._actions[_action]();
        }
    }

    public string NextAction()
    {
        foreach (Instruction instruction in _instructions)
        {
            if (Verify(instruction)) { return instruction._stringAction; }
        }
        return "ACTION_IDLE";
    }

    bool Verify(Instruction instruction)
    {
        bool flag = true;
        foreach (string percept in instruction._listeStringPerceptsVoulus)
        {
            if (!(_percepts._percepts.ContainsKey(percept) && _percepts._percepts[percept]())) { flag = false; }
        }

        // bool flag2 = false;
        // foreach (string percept in instruction._listeStringPerceptsOu)
        // {
        //     if (!(_percepts._percepts.ContainsKey(percept) && _percepts._percepts[percept]())) { flag2 = true; }
        // }
        // return (flag && flag2);

        return flag;
    }



}
