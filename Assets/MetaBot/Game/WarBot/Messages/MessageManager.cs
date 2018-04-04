using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MessageManager : MonoBehaviour {

    public GameObject sender;
    public List<Message> _waitingMessages = new List<Message>();
    public List<Message> _currentMessages = new List<Message>();
    public string[] _messageType;

    public abstract void Init();

    public void UpdateMessage()
    {
        _currentMessages = _waitingMessages;

        _waitingMessages = new List<Message>();
    }
    

    public void Send(Message message, string dest)
    {
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            if (unit.GetComponent<Stats>()._teamIndex == GetComponent<Stats>()._teamIndex && 
                ((unit.GetComponent<Stats>()._unitType == dest) || (dest == "All")))
            {

                unit.GetComponent<MessageManager>()._waitingMessages.Add(message);
            }
        }
    }

    public void Send(Message message, int destID)
    {
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            if (unit.GetComponent<Stats>()._teamIndex == GetComponent<Stats>()._teamIndex && unit.GetComponent<Stats>()._id == destID)
            {
                unit.GetComponent<MessageManager>()._waitingMessages.Add(message);
            }
        }
    }

    public Message ContainsType(string type)
    {
        foreach (Message m in _currentMessages)
        {
            if (m._titre == type) return m;
        }
        return null;
    }






}
