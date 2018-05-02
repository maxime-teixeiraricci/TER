using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour {

    Button playButton;
    Button editButton;
    Button quitButton;
    Button reloadButton;

    AudioSource gm;

    public string language;
    string langue;
    ChangeLanguage changeLanguage;
    
    public GameObject window;

 

    public Slider music;
    public InputField timeLimit;
    public InputField ressourceLimit;

    public Dropdown dropdown;
    public Dropdown gamemodeDrop;
    string gamemode;

    int time;
    int ressource;
    // Use this for initialization
    void Start () {
        changeLanguage = GetComponent<ChangeLanguage>();
        gm = GameObject.Find("GameManager").GetComponent<AudioSource>();
        language = GameObject.Find("GameManager").GetComponent<LangageLoader>().language;
    }

  

    public void DisplaySettings()
    {
        setButtonInactive();
        window.SetActive(true);  
    }

    public void QuitSettings()
    {
        setButtonActive();
        window.SetActive(false);
    }

    public void ApplySettings()
    {
        setButtonActive();
        manageVolume();
        window.SetActive(false);
        changeLanguage.ChangementLangue(language);
        changeGameMode();
    }

    public void changeGameMode()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>()._gameName = gamemodeDrop.captionText.text;
        print("AA");
        ressource = int.Parse(ressourceLimit.text);
        print("AAAAA");
        time = int.Parse(timeLimit.text);
        GameObject.Find("GameManager").GetComponent<GameManager>().ressourceLimit = ressource;
        GameObject.Find("GameManager").GetComponent<GameManager>().timeLimit = time;
    }
    public void setLanguageFR()
    {
        language = "francais";
    }

    public void setLanguageEN()
    {
        language = "english";
    }

    public void manageVolume()
    {
        float volume = music.value / 100;
        Debug.Log("VOLUME = " + volume);
        gm.volume = volume;
    }

    void setButtonActive()
    {
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        editButton = GameObject.Find("EditButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        reloadButton = GameObject.Find("ReloadButton").GetComponent<Button>();
        playButton.interactable = true;
        editButton.interactable = true;
        quitButton.interactable = true;
        reloadButton.interactable = true;
    }

    void setButtonInactive()
    {
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        editButton = GameObject.Find("EditButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        reloadButton = GameObject.Find("ReloadButton").GetComponent<Button>();
        playButton.interactable = false;
        editButton.interactable = false;
        quitButton.interactable = false;
        reloadButton.interactable = false;
    }
}
