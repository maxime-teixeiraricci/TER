using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public GameObject loadingScreenBar;
    public Slider sliderLoad;
    public GameObject[] _DropDownList;
    public GameObject _numberplayerDropDown;
    public Color[] playerColor;
    public int nbPlayers;
    public Dropdown mapChoice;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void StartGame()
    {
        
        string s = mapChoice.captionText.text;
        nbPlayers = int.Parse(_numberplayerDropDown.GetComponent<Dropdown>().captionText.text);
        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();
        GameObject gameManager = GameObject.Find("GameManager");
        string gamePath = Application.streamingAssetsPath + "/teams/" + gameManager.GetComponent<GameManager>()._gameName + "/";
        
        gameManager.GetComponent<TeamManager>()._teams = new List<Team>();

        for (int i = 0; i < nbPlayers; i++)
        {
            Team team = new Team();
            team._color = playerColor[i];
            team._name = _DropDownList[i].GetComponent<Dropdown>().captionText.text.Replace("_", " ");
            team._unitsBehaviour = interpreter.xmlToBehavior(gamePath + team._name, gamePath);
            gameManager.GetComponent<TeamManager>()._teams.Add(team);
        }
        
        if (s == "Standard")
        {
            StartCoroutine(AsynchronousLoad(1));
            //SceneManager.LoadScene(1);
        }
        else if(s == "Test")
        {
            Debug.Log("BEfore load");
            StartCoroutine(AsynchronousLoad(3));
            Debug.Log("After load");
            //SceneManager.LoadScene(3);
        }
        else if (s == "Plaine")
        {
            StartCoroutine(AsynchronousLoad(4));
            //SceneManager.LoadScene(4);
        }
        else if (s == "Desolate")
        {
            StartCoroutine(AsynchronousLoad(5));
            //SceneManager.LoadScene(4);
        }
        //SceneManager.LoadScene(id);
    }

    IEnumerator AsynchronousLoad(int scene)
    {
        loadingScreenBar.SetActive(true);
        yield return null;
        Debug.Log("Here !");

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            sliderLoad.value = ao.progress;
            // [0, 0.9] > [0, 1]
            //float progress = Mathf.Clamp01(ao.progress / 0.9f);
            //Debug.Log("Loading progress: " + (progress * 100) + "%");

            // Loading completed
            if (ao.progress == 0.9f)
            { 
               sliderLoad.value = 1f;
               ao.allowSceneActivation = true;

            }

            yield return null;
        }
    }
}
