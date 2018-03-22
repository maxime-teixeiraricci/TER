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

    public List<Instruction> _instructions;
    public Percept _percepts;
    public ActionUnit _actions;
    private string _currentAction;
    public MessageManager _messageManager;
    
    void Start()
    {
        //GameObject.Find("Canvas").GetComponent<HUDManager>().CreateHUD(gameObject);
        GameObject.Find("Canvas").GetComponent<HUDManager>().CreateHUD(gameObject);
        _instructions = GameObject.Find("GameManager").GetComponent<TeamManager>().getUnitsBevahiours(GetComponent<Stats>()._teamIndex, GetComponent<Stats>()._unitType);
        print("Nombre Instruction : " + _instructions.Count + "[" + GetComponent<Stats>()._unitType + "]");
        _percepts = GetComponent<Percept>();
        _actions = GetComponent<ActionUnit>();
        //_messageManager = new MessageManager(this.gameObject);
        _messageManager = GetComponent<MessageManager>();
    }

    void FixedUpdate()
    {
        if (_instructions != null)
        {
            string _action = NextAction();
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
