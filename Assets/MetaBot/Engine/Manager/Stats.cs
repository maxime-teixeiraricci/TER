using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stats : MonoBehaviour
{
    [Header("Unit type")]
    public string _unitType;
    public int _teamIndex;
    public long _id;
    public GameObject _target;
    public float distanceToTarget;
    public Objet _objectToUse;
    public GameObject _bullet;
    public Contract _contract;

    [Header("Stats")]
    public float _heading;
    public bool _isBlocked;
    public int _health;
    public int _maxHealth;
    public float _reloadTime;
    //public Vector3 _target;
    public GameObject _objectif;

    void Awake()
    {
        _id = Random.Range(0, int.MaxValue);
    }

    void Start()
    {
        _heading = Random.Range(0, 360);
    }

    void Update()
    {
        //_reloadTime -= Time.deltaTime;
        if (_target != null)
        {
            distanceToTarget = Vector3.Distance(_target.transform.position, transform.position);
        }
        _heading = (_heading + 360) % 360;
        _health = Mathf.Min(_health, _maxHealth);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _heading , transform.eulerAngles.z);
        if (GetComponent<MovableCharacter>())
        {
            _isBlocked = GetComponent<MovableCharacter>()._isblocked;
        }
        if (_isBlocked)
        {
            //_heading = Random.Range(0, 360);
        }

        if (_health <= 0)
        {
          Destroy(gameObject);
        }
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    
}
