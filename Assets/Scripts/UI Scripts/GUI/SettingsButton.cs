using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{

    Button playButton;
    Button editButton;
    Button quitButton;
    Button reloadButton;
    Button settingsButton;
    public InputField nbrResources;
    public GameObject errorText;

    AudioSource gm;

    public int _maxResources;

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

    GameObject ressourceGameMode;

    int time;
    int ressource;
    // Use this for initialization
    void Start()
    {
        changeLanguage = GetComponent<ChangeLanguage>();
        gm = GameObject.Find("GameManager").GetComponent<AudioSource>();
        music.value = gm.volume*100;
        //manageVolume(); print("music.value == "+music.value);
        language = GameObject.Find("GameManager").GetComponent<LangageLoader>().language;
        ressourceGameMode = GameObject.Find("RessourceRaceControl");
        _maxResources = GameObject.Find("GameManager").GetComponent<GameManager>()._maxResources;
        nbrResources.text = Convert.ToString(GameObject.Find("GameManager").GetComponent<GameManager>()._maxResources);
    }



    void Update()
    {

        if (gamemodeDrop.captionText.text == "RessourceRace" && ressourceGameMode.activeSelf == false)
        {
            ressourceGameMode.SetActive(true);
        }
        else if (gamemodeDrop.captionText.text != "RessourceRace" && ressourceGameMode.activeSelf == true)
        {
            ressourceGameMode.SetActive(false);
        }
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
        changeLanguage.ChangementLangue(language);
        changeGameMode();
        if(numberResources() == 0)
        {
            numberResources();
        }
        else
        {
            return;
        }
        window.SetActive(false);
    }

    public void changeGameMode()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().wincondition = gamemodeDrop.captionText.text;
        ressource = int.Parse(ressourceLimit.text);
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
        gm.volume = volume;
    }


    // Repasse les boutons inactifs en actifs. Ils reprennent donc leur comportement classique
    void setButtonActive()
    {
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        editButton = GameObject.Find("EditButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        reloadButton = GameObject.Find("ReloadButton").GetComponent<Button>();
        settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
        playButton.interactable = true;
        editButton.interactable = true;
        quitButton.interactable = true;
        reloadButton.interactable = true;
        settingsButton.interactable = true;
    }

    // Rend les boutons en arrière plans "inactifs", ils ne réagissent donc plus si on clique dessus
    void setButtonInactive()
    {
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        editButton = GameObject.Find("EditButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        reloadButton = GameObject.Find("ReloadButton").GetComponent<Button>();
        settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
        playButton.interactable = false;
        editButton.interactable = false;
        quitButton.interactable = false;
        reloadButton.interactable = false;
        settingsButton.interactable = false;
    }

    int numberResources()
    {
       // GameSettings gs = GameObject.Find("GameSettingHUD").GetComponent<GameSettingManager>().GetSettings();

        string valueInput = nbrResources.text;
        List<int> listInt = new List<int>();
        for (int i = 0; i < valueInput.Length; i++)
        {
            listInt.Add(Convert.ToInt32(valueInput[i]));
        }

        for (int i = 0; i < listInt.Count; i++)
        {
            // La liste de caractères entrés par l'utilisateur
            int result = listInt[i];
            // Si le caractère courant n'est pas compris entre 0 et 9, ou s'il est égal à 0, et que la liste ne contient qu'un seul caractère
            
            //Debug.Log("listInt = " + listInt[i + 1]);
            
            /*
            if(listInt[0] == 52)
            {
                if(listInt[i+1] >48 )
                {
                    // On affiche un message d'erreur
                    errorText.SetActive(true);
                    Text error = errorText.GetComponentInChildren<Text>();
                    error.text = "Valeur incorrecte !";
                    _maxResources = -1;
                    return -1;
                }
            }*/
            if (valueInput.Length > 2 || ((listInt[0] == 52) && (listInt[1] > 48)) || (result > 57) || (result < 48) || ((result == 48) && (valueInput.Length == 1)) || ((listInt[0] >52) && (valueInput.Length > 1)) )
            {
                // On affiche un message d'erreur
                Debug.Log("index 0 = " + listInt[0]);
                Debug.Log("index 1 = " + listInt[1]);
                Debug.Log("TailleList = " + valueInput.Length);
                errorText.SetActive(true);
                Text error = errorText.GetComponentInChildren<Text>();
                error.text = "Valeur incorrecte !";
                _maxResources = -1;
                return -1;
            }
        }

        int max = int.Parse(nbrResources.text);
        _maxResources = max;
        GameObject.Find("GameManager").GetComponent<GameManager>()._maxResources = max;
        errorText.SetActive(false);
        return 0;
    }
}

