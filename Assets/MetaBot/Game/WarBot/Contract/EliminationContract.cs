using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminationContract : Contract
{
    public GameObject _target;

    public EliminationContract(GameObject target)
    {
        type = "Elimination";
        _target = target;
    }
    public EliminationContract()
    {
    }

   
}
