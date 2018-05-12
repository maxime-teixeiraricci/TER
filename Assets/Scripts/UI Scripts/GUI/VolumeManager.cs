using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour {

    // La source audio
    AudioSource gm;

    // Le slider qui gère la valeur du son
    public Slider music;

    // La valeur du son avant que l'on ait mute le son
    float formerValue;

    // Les sprites pour l'icone du son
    public Sprite muteSprite;
    public Sprite demuteSprite;

    // Use this for initialization
    void Start ()
    {
        gm = GameObject.Find("GameManager").GetComponent<AudioSource>();
        music.value = gm.volume*100;
    }

    // Ajuste la valeur du son (AudioSource) à la valeur du slider
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

    // Met la valeur du son à 0 lorsqu'on clique sur l'icone haut parleur, la repasse à sa valeur précédente non nulle lors d'un nouveau clic
    public void MuteDemute()
    {

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

    // Gère l'affichage du sprite de l'icone du son (Muted / not Muted)
    public void changeSprite()
    {
        Sprite currentSprite = GameObject.Find("MuteDemute").GetComponent<Image>().sprite;

        if(currentSprite.name == "soundMuted")
        {
            GameObject.Find("MuteDemute").GetComponent<Image>().sprite = demuteSprite;
        }
        else if(currentSprite.name == "soundIcon")
        {
            GameObject.Find("MuteDemute").GetComponent<Image>().sprite = muteSprite;
        }
    }
}
