using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public GameObject loadingScreenBar;
    public Slider sliderLoad;
    public TeamMenuHUD[] _DropDownList;
    public GameObject _numberplayerDropDown;
    public Color[] playerColor;
    public int nbPlayers;

    public void StartGame()
    {
        
        nbPlayers = int.Parse(_numberplayerDropDown.GetComponent<Dropdown>().captionText.text);
        XMLWarbotInterpreter interpreter = new XMLWarbotInterpreter();

        GameObject gameManager = GameObject.Find("GameManager");
        string gamePath = Application.streamingAssetsPath + "/teams/" + gameManager.GetComponent<GameManager>()._gameName + "/";
        
        gameManager.GetComponent<TeamManager>()._teams = new List<Team>();

        for (int i = 0; i < nbPlayers; i++)
        {
            Team team = new Team();
            team._color = _DropDownList[i]._teamColor;
            team._name = _DropDownList[i]._teamName;//.Replace("_", " ");
            team._unitsBehaviour = interpreter.xmlToBehavior(gamePath + team._name, gamePath);
            gameManager.GetComponent<TeamManager>()._teams.Add(team);
        }

        GameManager setting = gameManager.GetComponent<GameManager>();
        setting.SetSetting();

        //
        StartCoroutine(AsynchronousLoad(setting._gameSettings._indexSceneMap));
    }

    IEnumerator AsynchronousLoad(int scene)
    {
        loadingScreenBar.SetActive(true);
        yield return null;

        
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
