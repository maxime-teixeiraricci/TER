using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float _lifeTime;
    public GameObject _owner;
    public Vector3 _vect;
    public float _speed;
    public int _damage;
    public GameObject Explosion;

    void FixedUpdate ()
    {
        transform.position += _vect.normalized * Time.deltaTime * _speed;
        _lifeTime -= Time.deltaTime;
        if (_lifeTime < 0f)
        {
            GameObject explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(explosion, 1.5f);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        print(other + " Touched");
        if (!other.isTrigger && other.GetComponent<Stats>() && other.gameObject != _owner.gameObject)
        {
            print(other + " Touched");
            GameObject explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
            other.GetComponent<Stats>()._health -= _damage;
            _damage = 0;

            Destroy(gameObject, 0.25f);
            Destroy(explosion,1.5f);
        }
    }
}
