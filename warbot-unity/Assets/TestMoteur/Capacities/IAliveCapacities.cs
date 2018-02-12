using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAliveCapacities : ICommonCapacities
{
    double getHealth();
    void setHealth(double health);

    double getMaxHealth();
    void setMaxHealth(double health);

    void havingDamage(double health);
    

	
}
