using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovableUnit : AbstractUnit, IMoveCapacities, IAliveCapacities
{
    
    public double GetAngle()
    {
        throw new System.NotImplementedException();
    }

    public double getHealth()
    {
        return base.health;
    }

    public int getID()
    {
        throw new System.NotImplementedException();
    }

    public double getMaxHealth()
    {
        throw new System.NotImplementedException();
    }

    public double getSpeed()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 getTargetDestination()
    {
        throw new System.NotImplementedException();
    }

    public void havingDamage(double health)
    {
        throw new System.NotImplementedException();
    }

    public void setAngle()
    {
        throw new System.NotImplementedException();
    }

    public void setHealth(double health)
    {
        throw new System.NotImplementedException();
    }

    public void setMaxHealth(double health)
    {
        throw new System.NotImplementedException();
    }

    public void setSpeed(double speed)
    {
        throw new System.NotImplementedException();
    }

    public void setTargetDestination(Vector3 destination)
    {
        throw new System.NotImplementedException();
    }
}
