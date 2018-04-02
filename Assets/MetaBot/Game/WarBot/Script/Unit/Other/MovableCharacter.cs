using System.Collections;
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
    public GameObject B;

    private Vector3 nextposition;
    	

    public void Move()
    {
        vectMov = Utility.vectorFromAngle(GetComponent<Stats>()._heading) ;
        nextposition = transform.position + vectMov.normalized * speed * Time.deltaTime;
        
        if (!_isblocked) { transform.position = nextposition; }
        _isblocked = isBlocked();
        _obstacleEncounter = false;
    }


    public bool isBlocked()
    {
        vectMov = Utility.vectorFromAngle(GetComponent<Stats>()._heading);
        nextposition = transform.position + vectMov.normalized * speed * Time.deltaTime;
        Ray rA = new Ray(nextposition + vectMov.normalized * _offsetGround, Vector3.down * _edgeDistance);
        Ray rB = new Ray(nextposition + vectMov.normalized * _offsetObstacle, transform.right* 0.25f);

        RaycastHit hit;
        a = Physics.Raycast(rA.origin,rA.direction, rA.direction.magnitude);
        b = Physics.Raycast(rB, out hit, rB.direction.magnitude);
        Debug.DrawRay(rB.origin, rB.direction, Color.black);
        Debug.DrawRay(rA.origin, rA.direction, Color.red);


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
