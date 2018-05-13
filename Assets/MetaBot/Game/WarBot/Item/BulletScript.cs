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

    public AudioClip _shotSongStart;
    public AudioClip _shotSongFinish;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GameObject.Find("GameManager").GetComponent<AudioSource>();
        if (_shotSongStart != null) audioSource.PlayOneShot(_shotSongStart);
    }

    void FixedUpdate ()
    {
        transform.position += _vect.normalized * Time.deltaTime * _speed;
        _lifeTime -= Time.deltaTime;
        if (_lifeTime < 0f)
        {
            GameObject explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
            if (_shotSongFinish != null) audioSource.PlayOneShot(_shotSongFinish);
            Destroy(explosion, 1.5f);
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.GetComponent<Stats>() && other.gameObject != _owner.gameObject)
        {

            GameObject explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
            other.GetComponent<Stats>().AddHealth(-_damage);
            _damage = 0;
            if (_shotSongFinish != null) audioSource.PlayOneShot(_shotSongFinish);
            Destroy(explosion,1.5f);
            Destroy(gameObject, 0.25f);
        }
    }
}
