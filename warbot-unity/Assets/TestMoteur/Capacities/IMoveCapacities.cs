using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveCapacities : ICommonCapacities
{
    void setTargetDestination(Vector3 destination);
    Vector3 getTargetDestination();

    void setSpeed(double speed);
    double getSpeed();
	
}
