using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour {

    AudioSource gm;
    public Slider music;

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
        float volume = music.value / 100;
        gm.volume = volume;
    }
}
