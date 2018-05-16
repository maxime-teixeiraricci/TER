using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MessageManager : MonoBehaviour {

    public GameObject sender;
    public List<Message> _waitingMessages = new List<Message>();
    public List<Message> _currentMessages = new List<Message>();
    public string[] _messageType;
    public GameObject _messageLineObject;

    private Dictionary<GameObject,GameObject> _messageLineObjects = new Dictionary<GameObject, GameObject>();

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
            if (!unit.Equals(this.gameObject) &&unit.GetComponent<Stats>()._teamIndex == GetComponent<Stats>()._teamIndex && 
                ((unit.GetComponent<Stats>()._unitType == dest) || (dest == "All")))
            {


                Message M = new Message(message);
                CreateMessageLineObject(unit);
                M._receiver = unit;
                M.heading = Utility.getAngle(unit, gameObject);
                unit.GetComponent<MessageManager>()._waitingMessages.Add(M);
            }
        }
    }


    public void CreateMessageLineObject(GameObject unit)
    {
        if ((!_messageLineObjects.ContainsKey(unit)) || (_messageLineObjects.ContainsKey(unit) &&_messageLineObjects[unit] == null))
        { 
            GameObject messageLine = Instantiate(_messageLineObject);
            messageLine.GetComponent<MessageLineScript>()._sender = this.gameObject;
            messageLine.GetComponent<MessageLineScript>()._receiver = unit;
            _messageLineObjects[unit] = messageLine;
        }
    }

    public void Send(Message message, GameObject dest)
    {
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            if (unit.GetComponent<Stats>()._teamIndex == GetComponent<Stats>()._teamIndex && unit == dest)
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
