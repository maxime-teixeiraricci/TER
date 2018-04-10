﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableCharacter : MonoBehaviour
{
    public float speed;
    public Vector3 vectMov;
    public bool _isblocked;
    public float _offsetGround;
    public float _offsetObstacle;
    public float _edgeDistance;
    public bool _obstacleEncounter;

    public bool a;
    public bool b;
    public bool c;
    public bool d;
    public GameObject B;

    private Vector3 nextposition;
    	

    public void Move()
    {
        vectMov = Utility.vectorFromAngle(GetComponent<Stats>()._heading) ;
        nextposition = transform.position + vectMov.normalized * speed * Time.deltaTime;
        
        transform.position = nextposition;
        _isblocked = isBlocked();
        _obstacleEncounter = false;
    }


    public bool isBlocked()
    {
        vectMov = Utility.vectorFromAngle(GetComponent<Stats>()._heading);
        nextposition = transform.position + vectMov.normalized * speed * Time.deltaTime;
        Ray rA = new Ray(nextposition + vectMov.normalized * _offsetGround, Vector3.down * _edgeDistance);
        Ray rB = new Ray(nextposition + vectMov.normalized * _offsetObstacle, transform.right);
        Ray rC = new Ray(nextposition + vectMov.normalized * _offsetObstacle, Vector3.RotateTowards(transform.right, Vector3.right, Mathf.PI / 2, 0.0f));
        Ray rD = new Ray(nextposition + vectMov.normalized * _offsetObstacle, Vector3.RotateTowards(transform.right, Vector3.left, Mathf.PI/2, 0.0f));


        RaycastHit hit;
        RaycastHit hitL;
        RaycastHit hitR;
        a = Physics.Raycast(rA.origin,rA.direction, rA.direction.magnitude);
        b = Physics.Raycast(rB, out hit, rB.direction.magnitude);
        c = Physics.Raycast(rC, out hitR, rC.direction.magnitude);
        d = Physics.Raycast(rD, out hitL, rD.direction.magnitude);
        Debug.DrawRay(rB.origin, rB.direction, Color.black);
        Debug.DrawRay(rA.origin, rA.direction, Color.red);
        Debug.DrawRay(rC.origin, rC.direction, Color.grey);
        Debug.DrawRay(rD.origin, rD.direction, Color.cyan);

        B = null;
        if (b)
        {

            if (hit.transform.tag == "Unit" && hit.transform.gameObject != gameObject && !hit.collider.isTrigger)
            {
                Debug.DrawRay(rB.origin, rB.direction, Color.red);
                B = hit.transform.gameObject;
                return true;
            }
            else { Debug.DrawRay(rB.origin, rB.direction, Color.white);  }
        }
        if (c)
        {

            if (hitR.transform.tag == "Unit" && hitR.transform.gameObject != gameObject && !hitR.collider.isTrigger)
            {
                Debug.DrawRay(rC.origin, rC.direction, Color.red);
                B = hitR.transform.gameObject;
                return true;
            }
            else { Debug.DrawRay(rC.origin, rC.direction, Color.white); }
        }
        if (d)
        {

            if (hitL.transform.tag == "Unit" && hitL.transform.gameObject != gameObject && !hitL.collider.isTrigger)
            {
                Debug.DrawRay(rD.origin, rD.direction, Color.red);
                B = hitL.transform.gameObject;
                return true;
            }
            else { Debug.DrawRay(rD.origin, rD.direction, Color.white); }
        }
        if (!a) { return true; }
        
        return false ;

    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag != "Ground")
        {

            //_isblocked = true;
            //transform.position += (transform.position - other.gameObject.transform.position).normalized * 0.01f;
        }
    }

}
