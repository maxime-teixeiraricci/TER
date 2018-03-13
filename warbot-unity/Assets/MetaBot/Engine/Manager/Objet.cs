using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ajouter un objet")]
public class Objet : ScriptableObject
{
    public GameObject _gameObject;
    public ItemScript _itemScript;
    public string _name;
    public int _value;
    public int _size;
    public int _cost;

    public void getPicked()
    {
        
    }
}
