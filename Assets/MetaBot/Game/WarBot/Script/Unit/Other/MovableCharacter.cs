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
    public bool c;
    public bool d;
    public GameObject B;

    public GameObject collisionObject;

    private Vector3 nextposition;
    	
    public void Start()
    {
        _isblocked = isBlocked();
    }
    
    

    public void Move()
    {
        _isblocked = isBlocked();
        if (!isBlocked())
        {
            vectMov = Utility.vectorFromAngle(GetComponent<Stats>().GetHeading());
            nextposition = transform.position + vectMov.normalized * speed * 0.02f;

            transform.position = nextposition;
            
        }
        else
        {
            transform.position *= 1;
        }
        
    }

    public bool isBlocked()
    {
        return collisionObject != null;
    }
    /*
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
    */

    void OnCollisionStay(Collision other)
    {
        collisionObject = null;
        if (other.gameObject.tag != "Ground")
        {
           /* RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.right, 3.0F);
            Debug.DrawRay(transform.position, transform.right, Color.red);*/
            foreach (ContactPoint contact in other.contacts)
            {
                float a = Utility.getAngle(gameObject.transform.position, contact.point);
                float b = GetComponent<Stats>().GetHeading();
                float A = Mathf.Abs(a - b);
                float B = Mathf.Abs( 360+ Mathf.Min(a,b) - Mathf.Max(a, b) ) ;
                if (Mathf.Min(A, B) < 90f)
                {
                    collisionObject = other.transform.gameObject;
                    break;
                }
                
            }
            /*
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if (hit.transform.gameObject != gameObject)
                {
                    collisionObject = hit.transform.gameObject;
                }
            }*/
        }
    }

    void OnCollisionExit(Collision other)
    {
        collisionObject = null;
    }

}
