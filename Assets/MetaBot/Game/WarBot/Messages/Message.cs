using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message {

    public string _titre;
    public string _contenu;
    public GameObject _sender;
    public GameObject _receiver;

    public Message(GameObject sender, string titre, string contenu, GameObject receiver)
    {
        _titre = titre;
        _contenu = contenu;
        _sender = sender;
        _receiver = receiver;
    }

    public Message(GameObject sender, string titre)
    {
        _titre = titre;
        _contenu = "";
        _sender = sender;
    }
}
