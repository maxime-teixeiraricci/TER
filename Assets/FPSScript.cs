using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSScript : MonoBehaviour
{
    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        GetComponent<Text>().text = fps.ToString("00");

        if (fps < 30) GetComponent<Text>().color = Color.red;
        else if (fps < 60) GetComponent<Text>().color = Color.yellow;
        else GetComponent<Text>().color = Color.green;

    }

   
}