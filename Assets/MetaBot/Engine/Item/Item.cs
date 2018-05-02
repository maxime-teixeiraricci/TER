using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item
{
    [SerializeField]
    private int _cost;
    [SerializeField]
    private int _value;
    [SerializeField]
    private int _size;
    [SerializeField]
    private string _name;

    public Item(ItemBehavior i_behave)
    {
        _value = i_behave.getValue();
        _name = i_behave.getName();
        _size = i_behave.getSize();
        _cost = i_behave.getCost();
    }

    public Item(Item i)
    {
        _value = i.getValue();
        _name = i.getName();
        _size = i.getSize();
        _cost = i.getCost();
    }

    public Item(int val, int size, string name,int cost)
    {
        _value = val;
        _name = name;
        _size = size;
        _cost = cost;
    }

    public int getValue()
    {
        return _value;
    }

    public string getName()
    {
        return _name;
    }

    public int getSize()
    {
        return _size;
    }

    public int getCost()
    {
        return _cost;
    }

    public Item clone()
    {

        int size = _size;
        string name = _name;
        int cost = _cost;
        Item I = new Item(this);
        return I;
    }
}
