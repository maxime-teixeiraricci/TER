using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PerceptRadar : Percept
{

    void Start()
    {
        InitPercept();
    }


    public override void InitPercept()
    {
        base.InitPercept();

        _percepts["PERCEPT_ENEMY"] = delegate ()
        {

            Sight sight = GetComponent<Sight>();
            List<GameObject> _listOfUnitColl = new List<GameObject>();
            GetComponent<Stats>().SetTarget(null);
            foreach (GameObject gO in sight._listOfCollision)
            {
                if (gO)
                {
                    if (gO.GetComponent<Stats>() && gO.GetComponent<Stats>()._teamIndex != GetComponent<Stats>()._teamIndex)
                    {
                        GetComponent<Stats>().SetTarget(gO);
                      GetComponent<Stats>().SetHeading(getAngle(gO));
                        return true;
                    }
                }
            }
            return false;
        };

        _percepts["PERCEPT_ENEMY_LOW_LIFE"] = delegate ()
        {
            return _percepts["PERCEPT_ENEMY"]() && GetComponent<Stats>().GetTarget().GetComponent<Stats>().GetHealthRatio() <= 0.5f;


        };

}

    public int getAngle(GameObject _gameObject)
    {
        Vector3 vect = _gameObject.transform.position - transform.position;
        Vector3 projVect = Vector3.ProjectOnPlane(vect, Vector3.up);

        if (projVect.z > 0)
        {
            return (int)(360 - Vector3.Angle(projVect, new Vector3(1, 0, 0)));
        }
        return (int)(Vector3.Angle(projVect, new Vector3(1, 0, 0)));

    }
}
