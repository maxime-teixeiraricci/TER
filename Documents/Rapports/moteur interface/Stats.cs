using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stats : MonoBehaviour
{
    [Header("Unit type")]
    public string _unitType;
    public int _teamIndex;
    private GameObject _target;
    private float distanceToTarget;
    public Objet _objectToUse;
    public GameObject _bullet;
    private Contract _contract;
    private GameObject _targetContract;

    [Header("Stats")]
    private float _heading;
    private int _health;
    private int _maxHealth;
    private int _reloadTime { get; set; }
    

    [Header("Effects")]
    public GameObject _smallSmoke;
    public GameObject _largeSmoke;
    public GameObject _fire;
    public GameObject _explosion;

    public AudioClip _dieSong;

    private GameObject _currentEffect;

    AudioSource audioSource;
    GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        _health = _maxHealth;
        audioSource = gameManager.GetComponent<AudioSource>();
        SetHeading(Random.Range(0, 360));
        _smallSmoke.SetActive(true);
        _largeSmoke.SetActive(true);
        _fire.SetActive(true);
        _explosion.SetActive(true);

    }

    void Update()
    {
        if (_currentEffect)
        {
            _currentEffect.GetComponent<Transform>().position = transform.position;
        }
        
        if (_target != null)
        {
            distanceToTarget = Vector3.Distance(_target.transform.position, transform.position);
        }

    }






    public void AddContract(Contract contract)
    {
        _contract = contract;
        _targetContract = ((EliminationContract)_contract)._target;
    }

    public bool HaveContrat()
    {
        return _contract != null;
    }

    public void SetHeading(float heading)
    {
        _heading = (heading + 360) % 360;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _heading, transform.eulerAngles.z);
    }

    public float GetHeading()
    {
        return _heading;
    }

    public GameObject GetTarget()
    {
        return _target;
    }

    public Contract GetContract()
    {
        return _contract;
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public void SetMaxHealth(int value)
    {
        _maxHealth = value;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }
    public int GetHealth()
    {
        return _health;
    }
    public void SetHealth(int value)
    {
        _health = value;
        if (_health > _maxHealth) { _health = _maxHealth; }
        else if (_health <= 0)
        {
            if (_currentEffect)
            {
                Destroy(_currentEffect);
            }
            _currentEffect = Instantiate(_explosion, transform.position, Quaternion.identity);
            if (_dieSong != null) audioSource.PlayOneShot(_dieSong);
            Destroy(_currentEffect, 1.5f);
            Destroy(gameObject);

        }
    }
    
    public void AddHealth(int value)
    {
        _health = _health + value;
        SetSmoke();
        if (_health > _maxHealth) _health = _maxHealth;
        else if (_health <= 0)
        {
            if (_currentEffect)
            {
                Destroy(_currentEffect);
            }
            _currentEffect = Instantiate(_explosion, transform.position, Quaternion.identity);
            if (_dieSong != null) audioSource.PlayOneShot(_dieSong);
            Destroy(_currentEffect, 1.5f);
            Destroy(gameObject);
            
        }
    }

    public void SetReloadTime(int value)
    {
        _reloadTime = value;
    }

    public int GetReloadTime()
    {
        return _reloadTime;
    }

    void SetSmoke()
    {
        if (_currentEffect)
        {
            
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
    }

}
