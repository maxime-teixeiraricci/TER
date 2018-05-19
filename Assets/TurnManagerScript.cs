using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManagerScript : MonoBehaviour
{
    public float _timeTick;
    public float _ticksPerSeconds;
    
    // Update is called once per frame
	void FixedUpdate ()
    {
        _timeTick += 0.02F * Time.timeScale;
        _ticksPerSeconds = (1.0f / 0.02F) * Time.timeScale;
        if (_timeTick >= 0.04f)
        {
            _timeTick -= 0.04f;

            foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
            {
                if (unit.GetComponent<Brain>())
                {
                    unit.GetComponent<Brain>().UnitTurn();
                }
            }
        }
	}
}
