using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform[] _listOfStartPoint;
    public GameObject _weapon;
    private int _i;
    public int reloadTick = 0;
	
    public void Shoot()
    {
        if (reloadTick == 0)
        {
            GameObject weapon = Instantiate(_weapon);
            _i = (_i + 1) % _listOfStartPoint.Length;
            weapon.transform.position = _listOfStartPoint[_i].position;
            weapon.GetComponent<BulletScript>()._owner = gameObject;
            Transform canon = null;
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                if (transform.GetChild(i).transform.CompareTag("Canon"))
                {
                    canon = transform.GetChild(i);
                }
            }


            //If the child was found.
            if (canon != null)
            {
                weapon.GetComponent<BulletScript>()._vect = -canon.right;
            }
            else weapon.GetComponent<BulletScript>()._vect = Utility.vectorFromAngle(GetComponent<Stats>().GetHeading());
            reloadTick = GetComponent<Stats>().GetReloadTime();
        }
    }
}
