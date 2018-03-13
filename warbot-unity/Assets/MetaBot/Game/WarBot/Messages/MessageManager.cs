using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour {

    public GameObject sender;
    public List<Message> _messagesRecus = new List<Message>();
    public List<Message> _messages = new List<Message>();

    public void sendMessage(Message message)
    {

        message._receiver.GetComponent<MessageManager>()._messagesRecus.Add(message);

    }

    public void resetMessagesRecus()
    {

        _messagesRecus = new List<Message>();

    }

    public void createMessage(string titre, string contenu, GameObject receiver)
    {

        _messages.Add(new Message(sender, titre, contenu, receiver));

    }

    public void resetMessages()
    {

        _messages = new List<Message>();

    }

}
