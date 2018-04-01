using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour {

    public GameObject sender;
    public List<Message> _waitingMessages = new List<Message>();
    public List<Message> _currentMessages = new List<Message>();

    public void UpdateMessage()
    {
        _currentMessages = _waitingMessages;
        _waitingMessages = new List<Message>();
    }
    

}
