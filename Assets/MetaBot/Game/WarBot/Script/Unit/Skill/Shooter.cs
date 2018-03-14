using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform[] _listOfStartPoint;
    public GameObject _weapon;
    private int _i;
	
    public void Shoot()
    {
        print("Fire !!!");
        GameObject weapon = Instantiate(_weapon);
        _i = (_i + 1) % _listOfStartPoint.Length;
        weapon.transform.position = _listOfStartPoint[_i].position;
        weapon.GetComponent<BulletScript>()._owner = gameObject;
        weapon.GetComponent<BulletScript>()._vect = Utility.vectorFromAngle(GetComponent<Stats>()._heading);
        GetComponent<Stats>()._reloadTime = 2.5f;
    }
}
