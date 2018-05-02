using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public float _angle;
    public float _distance;
    public SphereCollider _sphereCollider;
    public Brain _brain;
    [Header("Object in Sight")]
    [SerializeField]
    public List<GameObject> _listOfCollision;


    // Use this for initialization
    void Start()
    {
        _sphereCollider.radius = _distance;
    }

    void Update()
    {
        for(int i = 0; i < 360; i += 10)
        {
            float h = GetComponent<Stats>().GetHeading();
            float j = i * Mathf.Deg2Rad;
            Vector3 A = Utility.vectorFromAngle(h).normalized * _distance;
            Vector3 B = new Vector3(Mathf.Cos(j), 0, Mathf.Sin(j)).normalized * _distance;

            float angle = Vector3.Angle(A, B);
            if (angle <= _angle)
            {
                //Debug.DrawLine(transform.position, transform.position + B.normalized * _distance, Color.blue);
            }
        }

       foreach (GameObject go in _listOfCollision)
        {
            if (go == null)
            {
                _listOfCollision.Remove(go);
                break;
            }
        }
    }

    
    void OnTriggerStay(Collider other)
    {
        if ((other.isTrigger && other.GetComponent<ItemHeldler>()) || !other.isTrigger)
        {

            float h = GetComponent<Stats>().GetHeading();
            Vector3 A = Utility.vectorFromAngle(h).normalized * _distance;
            Vector3 B = (other.transform.position - transform.position).normalized * _distance;

            float angle = Vector3.Angle(A, B);
            if (angle <= _angle && !_listOfCollision.Contains(other.gameObject))
            {

                _listOfCollision.Add(other.gameObject);
            }
            else if (angle > _angle && _listOfCollision.Contains(other.gameObject))
            {
                _listOfCollision.Remove(other.gameObject);
            }

            if (_listOfCollision.Contains(other.gameObject)) { Debug.DrawLine(transform.position, other.transform.position, Color.green); }
            else { Debug.DrawLine(transform.position, other.transform.position, Color.yellow); }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_listOfCollision.Contains(other.gameObject))
        {
            _listOfCollision.Remove(other.gameObject);
        }
    }
}
