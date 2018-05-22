using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSliderVal : MonoBehaviour {


	public int valSliderExplorer;
	public int valSliderHeavy;
	public int valSliderLight;
    public int valSliderMusic;
	
	public Slider exploSlider;
	public Slider heavySlider;
	public Slider lightSlider;
    public Slider musicSlider;
	// Use this for initialization
	void Start () {

		valSliderExplorer = (int)exploSlider.value;
		valSliderHeavy = (int)heavySlider.value;
		valSliderLight = (int)lightSlider.value;
        valSliderMusic = (int)musicSlider.value;
		
	}
}
