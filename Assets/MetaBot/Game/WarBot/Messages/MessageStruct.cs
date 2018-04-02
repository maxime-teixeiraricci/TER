using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MessageStruct {

    public string _intitule;
    public string _destinataire;

    public MessageStruct(string intitule, string destinataire)
    {
        _intitule = intitule;
        _destinataire = destinataire;
    }
}
