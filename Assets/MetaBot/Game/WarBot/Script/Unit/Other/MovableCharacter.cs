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

    private Vector3 nextposition;
    	

    public void Move()
    {
        vectMov = Utility.vectorFromAngle(GetComponent<Stats>()._heading) ;
        nextposition = transform.position + vectMov.normalized * speed * Time.deltaTime;
        _isblocked = isBlocked();
        if (!_isblocked) { transform.position = nextposition; }
        _obstacleEncounter = false;
    }


    public bool isBlocked()
    {
        Ray rA = new Ray(nextposition + vectMov.normalized * _offsetGround, Vector3.down * _edgeDistance);


        bool a = Physics.Raycast(rA.origin,rA.direction);
        
        if (!a) { Debug.DrawRay(rA.origin, rA.direction, Color.green); }
        else { Debug.DrawRay(rA.origin, rA.direction, Color.red); }
        
        return !a ;

    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag != "Ground")
        {

            _isblocked = true;
            transform.position += (transform.position - other.gameObject.transform.position).normalized * 0.01f;
        }
    }

}
