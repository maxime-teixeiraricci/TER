using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour{

    [SerializeField]
    public Dictionary<Objet,int> _objets = new Dictionary<Objet, int>();

    [SerializeField]
    public int _maxSize;
    [SerializeField]
    public int _actualSize;
    
    public Objet _ressource;

    void Update()
    {
        _actualSize = 0;
        foreach (Objet o in _objets.Keys)
        {
            _actualSize += _objets[o];
        }
    }

    public bool add(Objet obj)
    {
        if (_actualSize < _maxSize && obj._size <= (_maxSize - _actualSize))
        {
            if (_objets.ContainsKey(obj)) { _objets[obj] += 1; }
            else { _objets.Add(obj, 1); }
            _actualSize += obj._size;

            return true;
        }
        else return false;
    }

    public Objet find(string objectName)
    {
        foreach (Objet objet in _objets.Keys)
        {
            if (objet._name.Equals(objectName))
            {
                return objet;
            }
        }
        return null;
    }

    public bool pop(Objet obj)
    {
        if (_objets.ContainsKey(obj) && _objets[obj] > 0)
        {
            _objets[obj] -= 1;
            _actualSize -= obj._size;
            return true;
        }
        else return false;
    }

    public void use(string objectName)
    {
        ItemMethod _im = GameObject.Find("Item Manager").GetComponent<ItemMethod>();
        foreach (Objet objet in _objets.Keys)
        {
            if (objet._name.Equals(objectName) && _objets[objet] > 0)
            {
                _im._effects[objectName](gameObject);
                _objets[objet]--;
                break;
            }
        }
    }

    public bool isFull()
    {
        return _actualSize == _maxSize;
    }

    public bool isEmpty()
    {
        return _actualSize == 0;
    }
}
