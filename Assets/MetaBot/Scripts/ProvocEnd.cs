using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProvocEnd : MonoBehaviour
{

    public GameObject timer;
    bool test;
    // Use this for initialization
    void Start()
    {
        test = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!test)
        {
            if (timer.GetComponent<TimerScriptHUD>().timePassed > 30)
            {
              GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
              foreach (GameObject u in units)
              {
                 if (u.GetComponent<Stats>()._unitType.Equals("Base"))
                 {
                      if (u.GetComponent<Stats>()._teamIndex != 0)
                     {
                           Destroy(u);
                      }
                 }
              }
            }

        }
    }
}
