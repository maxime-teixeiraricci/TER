using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    static public Dictionary<string, Queue<GameObject>> dictPools = new Dictionary<string, Queue<GameObject>>();
    public Pool[] pools;

    static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }


        foreach(Pool pool in pools)
        {
            pool.prefab.SetActive(false);
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < pool.number; i ++)
            {
                GameObject instance = Instantiate(pool.prefab);
                instance.SetActive(false);
                instance.transform.parent = transform;
                queue.Enqueue(instance);
            }
            dictPools.Add(pool.tag, queue);
        }

    }


    static public GameObject Pick(string tag, Vector3 position, Quaternion rotation)
    {
        GameObject obj = dictPools[tag].Dequeue();
        if (obj)
        {
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            
        }
        return obj;
    }

    static public GameObject Pick(string tag, Vector3 position)
    {
        return Pick(tag, position, Quaternion.identity);
    }

    static public GameObject Pick(string tag)
    {
        return Pick(tag, Vector3.zero, Quaternion.identity);
    }





}


[System.Serializable()]
public struct Pool
{
    public string tag;
    public int number;
    public GameObject prefab;
}