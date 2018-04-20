using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour {

    Button playButton;
    Button editButton;
    Button quitButton;
    Button reloadButton;
    
    public GameObject window;

    public Slider explorer;
    public float oldValueExplo;

    public Slider heavy;
    float oldValueHeavy;

    public Slider warLight;
    float oldValueLight;

    public Dropdown dropdown;
    string nameMap;

    // Use this for initialization
    void Start () {
        oldValueExplo = explorer.maxValue / 2;
        oldValueHeavy = heavy.maxValue / 2;
        oldValueLight = warLight.maxValue / 2;
    }

    float getValueExplo()
    {
        return oldValueExplo;
    }

    public void DisplaySettings()
    {
        setButtonInactive();
        window.SetActive(true);  
    }

    public void QuitSettings()
    {
        setButtonActive();
        cancelChanges();
        window.SetActive(false);
    }

    public void ApplySettings()
    {
        setButtonActive();
        applyChanges();
        window.SetActive(false);
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

    void applyChanges()
    {
        oldValueExplo = explorer.value;
        oldValueLight = warLight.value;
        oldValueHeavy = heavy.value;
        nameMap = dropdown.captionText.text;
    }

    void cancelChanges()
    {
        explorer.value = oldValueExplo;
        warLight.value = oldValueLight;
        heavy.value = oldValueHeavy;
        if(nameMap == null)
        {
            nameMap = "Standard";
        }
        else
        {
            dropdown.captionText.text = nameMap;
        }
        
    }

}
