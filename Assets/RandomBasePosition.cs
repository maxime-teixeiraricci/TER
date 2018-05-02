using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBasePosition : MonoBehaviour
{
    public GameObject[] teams;

    void Awake()
    {
        for (int i = 0; i < teams.Length; i ++)
        {
            int j = Random.Range(0, teams.Length);

            GameObject t = teams[i];
            teams[i] = teams[j];
            teams[j] = t;
        }

        for (int i = 0; i < teams.Length; i++)
        {
            int j = Random.Range(0, teams.Length);
            teams[i].GetComponent<TeamPlayManagerScript>().teamIndex = i;
        }


    }
}
