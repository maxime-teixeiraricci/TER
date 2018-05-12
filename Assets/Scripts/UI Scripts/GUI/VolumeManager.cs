using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour {

    AudioSource gm;
    public Slider music;
    float formerValue;
    public Sprite muteSprite;
    public Sprite demuteSprite;

    // Use this for initialization
    void Start ()
    {
        gm = GameObject.Find("GameManager").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void AdjustSound()
    {
        if(music.value != 0)
        {
            float volume = music.value / 100;
            gm.volume = volume;
            formerValue = volume;
        }
        else
        {
            float volume = music.value / 100;
            gm.volume = volume;
        }
        
    }

    public void MuteDemute()
    {
        Debug.Log("Value gm Volume = " + gm.volume);
        Debug.Log("Value former = " + formerValue);
        if(gm.volume == 0)
        {
            gm.volume = formerValue / 100;
            music.value = gm.volume * 100;
        }
        else
        {
            formerValue = gm.volume * 100;
            gm.volume = 0;
            music.value = 0;
        }
    }

    public void changeSprite()
    {
        Sprite currentSprite = GameObject.Find("MuteDemute").GetComponent<Image>().sprite;
        //Debug.Log("Nom current sprite = " + currentSprite.name);
        if(currentSprite.name == "soundMuted")
        {
            GameObject.Find("MuteDemute").GetComponent<Image>().sprite = demuteSprite;
        }
        else if(currentSprite.name == "soundIcon")
        {
            GameObject.Find("MuteDemute").GetComponent<Image>().sprite = muteSprite;
        }

        //Debug.Log("Name currenSrpite AFTER Modif = " + currentSprite.name);
    }
}
