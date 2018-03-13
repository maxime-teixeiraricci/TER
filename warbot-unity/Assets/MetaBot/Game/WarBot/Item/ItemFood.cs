using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFood : Item
{
    public ItemFood(ItemBehavior i_behave) : base(i_behave)
    {
    }

    public ItemFood(Item i) : base(i)
    {
    }

    public ItemFood(int val, int size, string name, int cost) : base(val, size, name, cost)
    {
    }
}
