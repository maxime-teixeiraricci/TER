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
    public GameObject _targetContract;

    [Header("Stats")]
    public float _heading;
    public bool _isBlocked;
    public int _health;
    public int _maxHealth;
    public int _reloadTime;

    [Header("Effects")]
    public GameObject _smallSmoke;
    public GameObject _largeSmoke;
    public GameObject _fire;
    public GameObject _explosion;

    private GameObject _currentEffect;


    void Awake()
    {
        _id = Random.Range(0, int.MaxValue);
    }

    void Start()
    {
        _heading = Random.Range(0, 360);
        _smallSmoke.SetActive(true);
        _largeSmoke.SetActive(true);
        _fire.SetActive(true);
        _explosion.SetActive(true);

    }

    void Update()
    {
        if (_contract != null)
        {
            _targetContract = ((EliminationContract)_contract)._target;
        }
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

        if (_currentEffect)
        {
            _currentEffect.GetComponent<Transform>().position.Set(transform.position.x, transform.position.y, transform.position.z);
        }
        if (_health > _maxHealth * 0.75)
        {
            Destroy(_currentEffect);
        }
        else if (_health > _maxHealth * 0.50)
        {
            if (_currentEffect)
            {
                Destroy(_currentEffect);
            }
            _currentEffect = Instantiate(_smallSmoke, transform.position, Quaternion.identity);
        }
        else if (_health > _maxHealth * 0.25)
        {
            if (_currentEffect)
            {
                Destroy(_currentEffect);
            }
            _currentEffect = Instantiate(_largeSmoke, transform.position, Quaternion.identity);
        }
        else if (_health > 0)
        {
            if (_currentEffect)
            {
                Destroy(_currentEffect);
            }
            _currentEffect = Instantiate(_fire, transform.position, Quaternion.identity);
        }
        if (_health <= 0)
        {
            if (_currentEffect)
            {
                Destroy(_currentEffect);
            }
            _currentEffect = Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(_currentEffect, 1.5f);
        }
        if (transform.position.y < -5)
        {
            Destroy(_currentEffect);
            Destroy(gameObject);
        }
    }
}
