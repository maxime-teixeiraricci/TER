using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestTraduction : MonoBehaviour
{
    public Text[] listOfObject;
   


    void Awake()
    {
        
        listOfObject = Resources.FindObjectsOfTypeAll<Text>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }

    private void OnSceneUnloaded(Scene current)
    {
        Debug.Log("OnSceneUnloaded: " + current);
    }
}
