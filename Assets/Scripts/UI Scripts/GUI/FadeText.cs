using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class FadeText : MonoBehaviour {


    
    public GameObject objectSave;
    public Text saveText;
    private Color startColor;
    private Color endColor;
    float FadeoutTime;
    public float countdown;
    float timeRemaining;     


    public void DisplayText()
    {
        objectSave.SetActive(true);
        timeRemaining = countdown;
        StartCoroutine("LoseTime");
        Time.timeScale = 1; //Just making sure that the timeScale is right
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeRemaining--;
            if(timeRemaining <= 0)
            {
                objectSave.SetActive(false);
                break;
            }
        }
    }
}
