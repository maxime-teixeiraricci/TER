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

    void OnCollisionStay(Collision other)
    {
        collisionObject = null;
        if (other.gameObject.tag != "Ground")
        {
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
