using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public string _gameName;
    public int _minNumberOfTeam;
    public int _maxNumberOfTeam;

    [Header("Units")]
    public List<GameObject> _listUnitGameObject;
    public List<string> _listUnitName;
    public List<string> _listUnitPercepts;
    public List<string> _listUnitActions;

    public void Start()
    {
        string gamepath = "./teams/" + _gameName + "/";
        if (!Directory.Exists(gamepath))
        {
            //if it doesn't, create it
            Directory.CreateDirectory(gamepath);

        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
