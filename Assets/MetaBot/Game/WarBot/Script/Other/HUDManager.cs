using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HUDManager : MonoBehaviour
{
    public GameObject _HUDObject;
    public Image _StatsButton;
    public Image _SightButton;
    public Image _MessageButton;
    public Image _FPSCamButton;
    public Image _freeCamButton;

    private bool _isCreated;



    public bool messageOn;
    public bool sightOn;
    public bool statsOn;


    AudioSource audioSource;
    GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        audioSource = gameManager.GetComponent<AudioSource>();
        audioSource.clip = gameManager.GetComponent<GameManager>().audioWarbot;
        audioSource.Play();
    }

    void Update()
    {
        foreach (HP_HUDManager hud in Resources.FindObjectsOfTypeAll<HP_HUDManager>())
        {
            hud.transform.gameObject.SetActive(statsOn);
        }

        foreach (MessageLineScript message in Resources.FindObjectsOfTypeAll<MessageLineScript>())
        {
            message.transform.gameObject.SetActive(messageOn);
        }

        foreach (SightVisionScript sight in Resources.FindObjectsOfTypeAll<SightVisionScript>())
        {
            sight.transform.gameObject.SetActive(sightOn);
        }

        UpdateFreeCamColor();
        UpdateFPSCamColor();
    }

    public void MessageButton()
    {
        messageOn = !messageOn;
        _MessageButton.color = (messageOn) ? Color.green : Color.red;
    }

    public void FreeCamButton()
    {
        if (GameObject.Find("Main Camera").GetComponent<SubjectiveCamera>().stuck)
            GameObject.Find("Main Camera").GetComponent<SubjectiveCamera>().removeStuck();
        else if (!GameObject.Find("Main Camera").GetComponent<SubjectiveCamera>().fps)
            GameObject.Find("Main Camera").GetComponent<SubjectiveCamera>().goStuck();

       
    }

    void UpdateFreeCamColor()
    {
        _freeCamButton.color = (GameObject.Find("Main Camera").GetComponent<SubjectiveCamera>().stuck) ? Color.red : Color.green;
        if (GameObject.Find("Main Camera").GetComponent<SubjectiveCamera>().fps)
            _freeCamButton.color = Color.grey;
    }

    void UpdateFPSCamColor()
    {
        _FPSCamButton.color = (GameObject.Find("Main Camera").GetComponent<SubjectiveCamera>().fps) ? Color.red : Color.grey;
    }


    public void FPSCamButton()
    {
        if (GameObject.Find("Main Camera").GetComponent<SubjectiveCamera>().fps)
            GameObject.Find("Main Camera").GetComponent<SubjectiveCamera>().removeFPS(); 
    }

    public void StatsButton()
    {
        statsOn = !statsOn;
        _StatsButton.color = (statsOn) ? Color.green : Color.red;
    }

    public void SightButton()
    {
        sightOn = !sightOn;
        _SightButton.color = (sightOn) ? Color.green : Color.red;
    }


    public void CreateHUD(GameObject unit)
    {
        GameObject _hud = Instantiate(_HUDObject, transform);
        _hud.GetComponent<HP_HUDManager>()._target = unit;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
        audioSource.clip = gameManager.GetComponent<GameManager>().audioMenu;
        audioSource.Play();
    }


}
